using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Deadeye : MonoBehaviour
{
    [Header("shooting")]
    [SerializeField] private float _minTimeBetweenShoot = 5;
    [SerializeField] private float _maxTimeBetweenShoot = 9;
    [SerializeField] private float _pauseBeforeShoot = 3;

    [Header("settings")]
    [SerializeField] private DeadeyeBarrier _barrier;
    [SerializeField] private DeadeyeDrone[] _drones;
    [SerializeField] private float _maxOffsetY = 3f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _dronesRespawnTime = 3f;
    [SerializeField] private Health _health;

    private float _timeElapsed;
    private float _timeBetweenShoot;

    private float _maxOffsetX = 0;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _destination;
    private Coroutine _currentCoroutine;
    private int _diedDroneCounter;

    public event UnityAction Shooted;
    public event UnityAction StartShooting;

    private void OnEnable()
    {
        if (_startPosition == Vector3.zero)
            _startPosition = transform.position;

        _health.Died += OnDied;
        _health.SetHealth((int)(_health.Base + LevelManager.CurrentWave * LevelManager.BossBonusHealthPerLevel));

        _currentCoroutine = StartCoroutine(RandomMovingCoroutine());
        _timeBetweenShoot = Random.Range(_minTimeBetweenShoot, _maxTimeBetweenShoot);
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _timeBetweenShoot)
        {
            _timeElapsed = 0;
            _timeBetweenShoot = Random.Range(_minTimeBetweenShoot, _maxTimeBetweenShoot);
            StartShoot();
        }
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

    private void StartShoot()
    {
        StopCoroutine(_currentCoroutine);
        StartShooting?.Invoke();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(_pauseBeforeShoot);
        Shooted?.Invoke();
        _currentCoroutine = StartCoroutine(RandomMovingCoroutine());
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
        _barrier.gameObject.SetActive(true);

        foreach (var drone in _drones)
            drone.gameObject.SetActive(true);
    }

    public void OnDronDied()
    {
        _diedDroneCounter++;

        if (_diedDroneCounter == _drones.Length)
        {
            _barrier.gameObject.SetActive(false);
            StartCoroutine(RespawnCoroutine());
        }
    }
}
