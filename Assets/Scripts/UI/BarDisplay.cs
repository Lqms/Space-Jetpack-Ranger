using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _health;
    [SerializeField] private Image _energy;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.EnergyChanged += OnEnergyChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.EnergyChanged -= OnEnergyChanged;
    }

    private void OnHealthChanged(float newHealth)
    {
        _health.fillAmount = newHealth;
    }

    private void OnEnergyChanged(float newEnergy)
    {
        _energy.fillAmount = newEnergy;
    }
}
