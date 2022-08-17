using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float _maxEnemyCount = 3;
    [SerializeField] private int _enemiesToLevelUp = 10;
    [SerializeField] private int _bonusHealthPerLevel = 10;
    [SerializeField] private int _bonusDamagePerLevel = 5;
    [SerializeField] private int _waveMultiplicityToSave = 10;
    [SerializeField] private int _waveMultiplicityForBoss = 15;
    [SerializeField] private float _extraMaxEnemiesCountPerLevel = 0.1f;
    [SerializeField] private float _extraEnemiesToLevelUpPerLevel = 0.2f;

    public static DifficultyManager Instance { get; private set; }

    public event UnityAction<int> LevelUpped;
    public event UnityAction BossLevelUpped;

    private void OnEnable()
    {
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;

        if (Instance == this)
            DontDestroyOnLoad(this);
        else
            Destroy(this);

        ResetLevel();

        LevelManager.EnemiesMaxCount = _maxEnemyCount;
        LevelManager.EnemiesToLevelUp = _enemiesToLevelUp;
        LevelManager.BonusHealthPerLevel = _bonusHealthPerLevel;
        LevelManager.BonusDamagePerLevel = _bonusDamagePerLevel;
        LevelManager.WaveMultiplicityToSave = _waveMultiplicityToSave;
        LevelManager.WaveMultiplicityForBoss = _waveMultiplicityForBoss;
    }
    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
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
        LevelManager.CurrentWave++;
        PlayerManager.Instance.TrySaveWave();

        LevelManager.DefeatedEnemies = 0;
        LevelManager.EnemiesCount = 0;
        LevelManager.EnemiesMaxCount += _extraMaxEnemiesCountPerLevel;
        LevelManager.EnemiesToLevelUp += _extraEnemiesToLevelUpPerLevel;

        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityForBoss == 0)
        {
            BossLevelUpped?.Invoke();
        }
        else
        {
            LevelUpped?.Invoke(LevelManager.CurrentWave);
        }
    }

    public void ResetLevel()
    {
        PlayerManager.Instance.TrySaveMoney();
        LevelManager.CurrentWave = PlayerManager.Instance.Wave;
        LevelManager.DefeatedEnemies = 0;
        LevelManager.EnemiesCount = 0;
    }
}
