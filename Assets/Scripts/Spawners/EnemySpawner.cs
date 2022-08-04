using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Player _player;

    public override void Spawn(GameObject enemy)
    {
        if (LevelManager.EnemiesCount >= LevelManager.EnemiesMaxCount)
            return;

        base.Spawn(enemy);
        LevelManager.EnemiesCount++;
    }

    protected override void Setup(GameObject enemy, Vector2 position)
    {
        base.Setup(enemy, position);

        if (enemy.TryGetComponent(out Enemy enemyScript)) 
            enemyScript.Setup(_player, SpawnerContainer, _shootPoint);
    }
}
