using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    
    protected float Speed;
    protected Health Health;
    protected Player Target;
    protected ExplosionSpawner ExplosionSpawner;
    protected HealthSpawner HealthSpawner;
    protected Transform ShootPoint;

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

    public virtual void Setup(Player player, ExplosionSpawner explosionSpawner, Transform shootPoint, HealthSpawner healthSpawner)
    {
        Target = player;
        ExplosionSpawner = explosionSpawner;
        ShootPoint = shootPoint;
        HealthSpawner = healthSpawner;
    }

    protected virtual void OnDied()
    {
        HealthSpawner.SpawnHealth(transform.position, Health.Max);
        Speed = 0;
        LevelManager.EnemiesCount--;
        LevelManager.DefeatedEnemies++;
        Debug.Log("Defeated enemies " + LevelManager.DefeatedEnemies);
        gameObject.SetActive(false);
        Speed = _maxSpeed;
    }
}
