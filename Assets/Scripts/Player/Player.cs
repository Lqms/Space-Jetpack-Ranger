using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _percentOfRestoringHealth = 25;
    [SerializeField] private float _percentOfRestoringEnergy = 25;

    private Health _health;
    private Energy _energy;
    private PlayerCombat _combat;
    private PlayerMover _mover;

    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> EnergyChanged;
    public event UnityAction Died;

    private void OnEnable()
    {
        _combat = GetComponent<PlayerCombat>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
        _energy = GetComponent<Energy>();

        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        EnergyChanged?.Invoke(_energy.Current/_energy.Max);
    }

    public void ApplyDamage(float damage)
    {
        _health.ApplyDamage(damage);
        HealthChanged?.Invoke(_health.Current/_health.Max);
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
    }

    private void OnDied()
    {
        _combat.enabled = false;
        _mover.enabled = false;
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
