using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    
    protected Health Health;
    protected Player Target;
    protected SpawnerContainer SpawnerContainer;
    protected Transform ShootPoint;
    protected float Speed;

    protected virtual void OnEnable()
    {
        Speed = _maxSpeed;
        Health = GetComponent<Health>();
        Health.IncreaseMaxHealth(LevelManager.BonusHealthPerLevel * LevelManager.CurrentLevel);
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
        StopAllCoroutines();
    }

    public void Setup(Player target, SpawnerContainer spawnerContainer, Transform shootPoint)
    {
        Target = target;
        SpawnerContainer = spawnerContainer;
        ShootPoint = shootPoint;
    }

    protected virtual void OnDied()
    {
        Speed = 0;

        SpawnerContainer.HealthSpawner.Spawn(transform.position);
        LevelManager.EnemiesCount--;
        LevelManager.DefeatedEnemies++;

        gameObject.SetActive(false);
    }
}
