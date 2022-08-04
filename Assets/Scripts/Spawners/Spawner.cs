using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] protected SpawnerContainer SpawnerContainer;

    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private bool _isSpawningOnTimer;
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;

    private float _secondsBetweenSpawn;
    private float _elapsedTime;

    private void Start()
    {
        if (_isSpawningOnTimer)
            _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);

        Initialize(_objectPrefab);
    }

    private void Update()
    {
        if (_isSpawningOnTimer == false)
            return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
            if (TryGetObject(out GameObject obj))
                Spawn(obj);
    }

    public virtual void Spawn(GameObject obj)
    {
        _elapsedTime = 0;
        _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);
        Vector2 position = new Vector2(transform.position.x, Randomizer.RandomPositionY());
        Setup(obj, position);
    }

    public virtual void Spawn(Vector2 position)
    {
        if (TryGetObject(out GameObject obj))
            Setup(obj, position);
    }

    protected virtual void Setup(GameObject obj, Vector2 position)
    {
        obj.SetActive(true);
        obj.transform.position = position;
    }
}
