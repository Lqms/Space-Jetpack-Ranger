using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] protected Player Player;
    [SerializeField] protected GameObject ObjectPrefab;
    [SerializeField] protected float MinSecondsBetweenSpawn;
    [SerializeField] protected float MaxSecondsBetweenSpawn;
    [SerializeField] protected bool IsSpawningOnTimer;

    private float _secondsBetweenSpawn;
    private float _elapsedTime;
    private float _playerHeight;

    protected virtual void Start()
    {
        if (IsSpawningOnTimer)
            _secondsBetweenSpawn = Random.Range(MinSecondsBetweenSpawn, MaxSecondsBetweenSpawn);

        _playerHeight = Player.GetComponent<SpriteRenderer>().sprite.rect.size.y;
        Initialize(ObjectPrefab);
    }

    protected virtual void Update()
    {
        if (IsSpawningOnTimer == false)
            return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject obj))
            {
                Spawn(obj);
            }
        }
    }

    protected virtual void Spawn(GameObject obj)
    {
        _elapsedTime = 0;
        _secondsBetweenSpawn = Random.Range(MinSecondsBetweenSpawn, MaxSecondsBetweenSpawn);
        Setup(obj, new Vector2(transform.position.x, Randomizer.RandomPositionY()));
    }

    protected virtual void Setup(GameObject obj, Vector2 position)
    {
        obj.SetActive(true);
        obj.transform.position = position;
    }
}
