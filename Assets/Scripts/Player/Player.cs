using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxEnergy = 100;
    [SerializeField] private float _energyWastePerSecond = 2;

    private float _energy;

    private Rigidbody2D _rigidBody;
    private Health _health;
    private PlayerCombat _combat;
    private PlayerMover _mover;

    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> EnergyChanged;
    public event UnityAction Died;

    private const float GravityScale = 0.05f;

    private void OnEnable()
    {
        _combat = GetComponent<PlayerCombat>();
        _mover = GetComponent<PlayerMover>();
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
        _energy = _maxEnergy;
    }


    private void FixedUpdate()
    {
        if (_energy <= 0)
        {
            _rigidBody.gravityScale = GravityScale;
            return;
        }

        _energy -= Time.fixedDeltaTime * _energyWastePerSecond;
        EnergyChanged?.Invoke(_energy/_maxEnergy);
    }

    public void ApplyDamage(float damage)
    {
        _health.ApplyDamage(damage);
        HealthChanged?.Invoke(_health.Current/_health.Max);
    }

    public void RestoreHealth(float amountOfHeal)
    {
        _health.Heal(amountOfHeal);
        HealthChanged?.Invoke(_health.Current / _health.Max);
    }

    public void RestoreEnergy()
    {
        _energy = Mathf.Clamp(25, 0, _maxEnergy);

        if (_energy > 0)
            _rigidBody.gravityScale = 0;

        EnergyChanged?.Invoke(_energy / _maxEnergy);
    }

    private void OnDied()
    {
        _combat.enabled = false;
        _mover.enabled = false;
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
