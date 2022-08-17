using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreator : MonoBehaviour
{
    [SerializeField] private List<Boss> _bosses;
    [SerializeField] private Transform _bossShootPoint;

    private void OnEnable()
    {
        DifficultyManager.Instance.BossLevelUpped += OnBossLevelUpped;

        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityForBoss == 0)
            OnBossLevelUpped();
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.BossLevelUpped -= OnBossLevelUpped;
    }

    private void OnBossLevelUpped()
    {
        Boss boss = Instantiate(GetRandomBoss(), transform.position, Quaternion.identity);
        boss.MoveToPoint(_bossShootPoint);
    }

    private Boss GetRandomBoss()
    {
        int randomIndex = Random.Range(0, _bosses.Count);
        return _bosses[randomIndex];
    }
}
