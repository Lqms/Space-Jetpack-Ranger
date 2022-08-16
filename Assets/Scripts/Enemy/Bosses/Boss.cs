using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int _baseReward;
    [SerializeField] private Health _bossHealth;

    private void OnEnable()
    {
        _bossHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _bossHealth.Died -= OnDied;
    }

    private void OnDied()
    {
        PlayerManager.Instance.IncreaseMoney(_baseReward * LevelManager.CurrentWave);
        Destroy(gameObject);
    }
}
