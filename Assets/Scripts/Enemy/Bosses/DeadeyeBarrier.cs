using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeadeyeBarrier : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trails;

    private Health _health;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.Died += OnDied;
        _health.SetHealth((int)(_health.Base + LevelManager.CurrentWave * LevelManager.BossBonusHealthPerLevel));
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void SetEmissionValue(float value) /// хочу потихоньку уменьшать количество частиц при уничтожении каждого дрона
    {
        var emissionModule = _trails.emission;
        emissionModule.rateOverTime = value;
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
    }
}
