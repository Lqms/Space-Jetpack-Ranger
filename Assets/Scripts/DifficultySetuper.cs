using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultySetuper : MonoBehaviour
{
    [SerializeField] private float _maxEnemyCount = 3;
    [SerializeField] private int _enemiesToLevelUp = 10;
    [SerializeField] private int _bonusHealthPerLevel = 10;
    [SerializeField] private int _bonusDamagePerLevel = 5;

    public event UnityAction<int> LevelUpped;

    private void Awake()
    {
        LevelManager.CurrentLevel = 1;
        LevelManager.DefeatedEnemies = 0;
        LevelManager.EnemiesCount = 0;
        LevelManager.EnemiesMaxCount = _maxEnemyCount;
        LevelManager.EnemiesToLevelUp = _enemiesToLevelUp;
        LevelManager.BonusHealthPerLevel = _bonusHealthPerLevel;
        LevelManager.BonusDamagePerLevel = _bonusDamagePerLevel;
    }

    private void Update()
    {
        if (LevelManager.DefeatedEnemies > LevelManager.EnemiesToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        LevelManager.CurrentLevel++;
        LevelManager.DefeatedEnemies = 0;
        LevelManager.EnemiesCount = 0;
        LevelManager.EnemiesMaxCount += 0.1f;
        LevelManager.EnemiesToLevelUp += 1;

        LevelUpped?.Invoke(LevelManager.CurrentLevel);
        Debug.Log("Level " + LevelManager.CurrentLevel);
    }
}
