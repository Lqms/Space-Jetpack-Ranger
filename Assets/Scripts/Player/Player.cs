using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _percentOfRestoringHealth = 25;
    [SerializeField] private float _percentOfRestoringEnergy = 25;

    [SerializeField] private float _timeBeforeDeactivateJetpack = 5;
    [SerializeField] private float _gravityMultipler = 10;

    private Health _health;
    private Energy _energy;
    private PlayerCombat _combat;
    private PlayerInput _input;
    private Rigidbody2D _rigidBody;
    private Coroutine _countdownCorotine;

    public Health Health => _health;
    public Energy Energy => _energy;

    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> EnergyChanged;
    public event UnityAction JetpackDeactivated;

    private const float GravityScale = 0.05f;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;

        _combat = GetComponent<PlayerCombat>();
        _input = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();
        _energy = GetComponent<Energy>();

        _health.Died += OnDied;
        _health.Damaged += OnDamaged;

        _energy.RunOut += OnEnergyRunOut;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        _health.Damaged -= OnDamaged;
        _energy.RunOut -= OnEnergyRunOut;
    }

    private void FixedUpdate()
    {
        EnergyChanged?.Invoke(_energy.Current/_energy.Max);
    }

    public void RestoreHealth()
    {
        float amount = _percentOfRestoringHealth / 100 * _health.Max;
        _health.Restore(amount);
        HealthChanged?.Invoke(_health.Current / _health.Max);
    }

    public void RestoreEnergy()
    {
        float amount = _percentOfRestoringEnergy / 100 * _energy.Max;
        _energy.Restore(amount);
        EnergyChanged?.Invoke(_energy.Current / _energy.Max);

        if (_countdownCorotine != null)
        {
            StopCoroutine(_countdownCorotine);
            ActivateJetpack();
        }
    }

    private void OnDied()
    {
        _combat.enabled = false;
        _input.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDamaged()
    {
        HealthChanged?.Invoke(_health.Current / _health.Max);
    }

    private void OnEnergyRunOut()
    {
        if (_countdownCorotine != null)
            return;

        _rigidBody.gravityScale = GravityScale;
        _countdownCorotine = StartCoroutine(CountdownToDeactivateJetpackCoroutine());
    }

    private IEnumerator CountdownToDeactivateJetpackCoroutine()
    {
        float timer = _timeBeforeDeactivateJetpack;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        DeactivateJetpack();
    }

    private void DeactivateJetpack()
    {
        JetpackDeactivated?.Invoke();
        _input.enabled = false;
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale *= _gravityMultipler;
    }

    private void ActivateJetpack()
    {
        _input.enabled = true;
        _rigidBody.gravityScale = 0;
        _countdownCorotine = null;
    }
}
