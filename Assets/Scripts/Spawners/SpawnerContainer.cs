using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerContainer : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    [Header("Spawner objects")]
    [SerializeField] private BountyUISpawner _bountyUISpawner;
    [SerializeField] private EnergyObjectSpawner _energyObjectSpawner;
    [SerializeField] private EnergySpawner _energySpawner;
    [SerializeField] private EnergySuckerSpawner _energySuckerSpawner;
    [SerializeField] private ExplosionSpawner _explosionSpawner;
    [SerializeField] private HealthSpawner _healthSpawner;
    [SerializeField] private SpaceJuggernautSpawner _spaceJuggernautSpawner;
    [SerializeField] private SuiciderSpawner _suiciderSpawner;

    public BountyUISpawner BountyUISpawner => _bountyUISpawner;
    public EnergyObjectSpawner EnergyObjectSpawner => _energyObjectSpawner;
    public EnergySpawner EnergySpawner => _energySpawner;
    public EnergySuckerSpawner EnergySuckerSpawner => _energySuckerSpawner;
    public ExplosionSpawner ExplosionSpawner => _explosionSpawner;
    public HealthSpawner HealthSpawner => _healthSpawner;
    public SpaceJuggernautSpawner SpaceJuggernautSpawner => _spaceJuggernautSpawner;
    public SuiciderSpawner SuiciderSpawner => _suiciderSpawner;

    private void OnEnable()
    {
        DifficultyManager.Instance.BossLevelUpped += OnBossLevelUpped;
        DifficultyManager.Instance.LevelUpped += OnLevelUpped;
        Enemy.Died +=

        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityForBoss == 0)
            OnBossLevelUpped();
        else
            OnLevelUpped();
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.BossLevelUpped -= OnBossLevelUpped;
        DifficultyManager.Instance.LevelUpped -= OnLevelUpped;
    }

    private void OnBossLevelUpped()
    {
        foreach (var spawner in _spawners)
        {
            spawner.gameObject.SetActive(false);
        }
    }

    private void OnLevelUpped(int level = 0)
    {
        foreach (var spawner in _spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    private void OnEnemyDied()
    {
        HealthSpawner.Spawn(transform.position);
        BountyUISpawner.Spawn(transform.position);
        BountyUISpawner.Bounty.Setup(_currentBounty);
    }
}
