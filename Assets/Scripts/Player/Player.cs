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
    [SerializeField] private float _energy;
    [SerializeField] private float _energyWastePerSecond = 2;

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
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;

        _energy = _maxEnergy;
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnergyItem energy))
        {
            _energy = Mathf.Clamp(_energy + energy.AmountOfRestoredEnergy, 0, _maxEnergy);

            if (_energy > 0)
                _rigidBody.gravityScale = 0;

            EnergyChanged?.Invoke(_energy / _maxEnergy);
            energy.gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out HealthItem health))
        {
            Heal(health.AmountOfRestoredHealth);
            health.gameObject.SetActive(false);
        }
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

    public void Heal(float amountOfHeal)
    {
        _health.Heal(amountOfHeal);
        HealthChanged?.Invoke(_health.Current / _health.Max);
    }

    private void OnDied()
    {
        _combat.enabled = false;
        _mover.enabled = false;
        Died?.Invoke();
        gameObject.SetActive(false); // анимацию мб
    }
}
