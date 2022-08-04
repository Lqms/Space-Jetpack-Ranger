using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private ExplosionSpawner _explosionSpawner;
    [SerializeField] private HealthSpawner _healthSpawner;
    [SerializeField] private Transform _shootPoint;

    protected override void Spawn(GameObject enemy)
    {
        if (LevelManager.EnemiesCount >= LevelManager.EnemiesMaxCount)
            return;

        base.Spawn(enemy);
        LevelManager.EnemiesCount++;
        // Debug.Log(LevelManager.EnemiesCount);
    }

    protected override void Setup(GameObject enemy, Vector2 position)
    {
        base.Setup(enemy, position);

        if (enemy.TryGetComponent(out Enemy enemyScript))
            enemyScript.Setup(Player, _explosionSpawner, _shootPoint, _healthSpawner);
    }
}
