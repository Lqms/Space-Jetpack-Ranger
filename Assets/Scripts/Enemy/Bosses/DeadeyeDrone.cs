using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeadeyeDrone : MonoBehaviour
{
    [SerializeField] private Deadeye _deadeye;
    [SerializeField] private float _maxOffsetX = 1f;
    [SerializeField] private float _maxOffsetY = 1f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _respawnTime = 5f;
    [SerializeField] private Health _health;

    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _destination;
    private Coroutine _currentCoroutine;

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
        _deadeye.OnDronDied();
        StopCoroutine(_currentCoroutine);
        gameObject.SetActive(false);
    }
}
