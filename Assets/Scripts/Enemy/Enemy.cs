using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private int _basedBounty;
    
    protected Health Health;
    protected SpawnerContainer SpawnerContainer;
    protected Transform ShootPoint;
    protected float Speed;
    protected bool DestroyedByPlayer = true;

    private int _currentBounty;

    public Player Target { get; protected set; }

    public static event UnityAction<Enemy> Died;

    protected virtual void OnEnable()
    {
        _currentBounty = _basedBounty + LevelManager.CurrentWave * LevelManager.BonusBountyPerLevel;
        DestroyedByPlayer = true;
        Speed = _maxSpeed;
        Health = GetComponent<Health>();
        Health.IncreaseMaxHealth(LevelManager.BonusHealthPerLevel * LevelManager.CurrentWave);
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

        if (DestroyedByPlayer)
        {
            Died?.Invoke(this);

            /*
            SpawnerContainer.HealthSpawner.Spawn(transform.position);
            SpawnerContainer.BountyUISpawner.Spawn(transform.position);
            SpawnerContainer.BountyUISpawner.Bounty.Setup(_currentBounty);
            PlayerManager.Instance.IncreaseMoney(_currentBounty);
            */
        }

        LevelManager.EnemiesCount--;
        LevelManager.DefeatedEnemies++;

        gameObject.SetActive(false);
    }
}
