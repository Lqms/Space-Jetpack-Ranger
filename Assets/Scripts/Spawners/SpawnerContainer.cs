using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerContainer : MonoBehaviour
{
    [SerializeField] private EnergyObjectSpawner _energyObjectSpawner;
    [SerializeField] private EnergySpawner _energySpawner;
    [SerializeField] private ExplosionSpawner _explosionSpawner;
    [SerializeField] private HealthSpawner _healthSpawner;
    [SerializeField] private SpaceJuggernautSpawner _spaceJuggernautSpawner;
    [SerializeField] private SuiciderSpawner _suiciderSpawner;

    public EnergyObjectSpawner EnergyObjectSpawner => _energyObjectSpawner;
    public EnergySpawner EnergySpawner => _energySpawner;
    public ExplosionSpawner ExplosionSpawner => _explosionSpawner;
    public HealthSpawner HealthSpawner => _healthSpawner;
    public SpaceJuggernautSpawner SpaceJuggernautSpawner => _spaceJuggernautSpawner;
    public SuiciderSpawner SuiciderSpawner => _suiciderSpawner;
}
