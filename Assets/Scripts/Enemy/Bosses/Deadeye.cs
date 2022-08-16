using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Deadeye : MonoBehaviour
{
    [SerializeField] private Drone[] _drones;
    [SerializeField] private float _maxOffsetY = 3f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _dronesRespawnTime = 3f;
    [SerializeField] private Health _health;

    private float _maxOffsetX = 0;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _destination;
    private Coroutine _currentCoroutine;
    private int _diedDroneCounter;

    private void OnEnable()
    {
        if (_startPosition == Vector3.zero)
            _startPosition = transform.position;

        _health.Died += OnDied;
        _currentCoroutine = StartCoroutine(RandomMovingCoroutine());
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private IEnumerator RandomMovingCoroutine()
    {
        while (true)
        {
            _destination = Randomizer.RandomChangePosition(_startPosition, _maxOffsetX, _maxOffsetY);

            while (transform.position != _destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
                yield return null;
            }

            while (transform.position != _startPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void OnDied()
    {
        StopCoroutine(_currentCoroutine);
        gameObject.SetActive(false);
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(_dronesRespawnTime);

        _diedDroneCounter = 0;

        foreach (var drone in _drones)
            drone.gameObject.SetActive(true);
    }

    public void OnDronDied()
    {
        _diedDroneCounter++;

        if (_diedDroneCounter == _drones.Length)
            StartCoroutine(RespawnCoroutine());
    }
}
