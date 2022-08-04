using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJuggernautMover : Enemy
{
    [SerializeField] private float _minSecondsBetweenChangeBehaviour;
    [SerializeField] private float _maxSecondsBetweenChangeBehaviour;

    private float _secondsBetweenChangeBehaviour;
    private float _timeElapsed;
    private int _numberOfBehaviour;
    private bool _isShootPointReached = false;
    private Coroutine _activeCoroutine;

    protected override void OnEnable()
    {
        base.OnEnable();
        ResetParam();
    }

    private void Update()
    {
        if (_isShootPointReached == false)
            MoveToShootPoint();

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _secondsBetweenChangeBehaviour)
        {
            _timeElapsed = 0;
            _secondsBetweenChangeBehaviour = Random.Range(_minSecondsBetweenChangeBehaviour, _maxSecondsBetweenChangeBehaviour);
            _numberOfBehaviour = Random.Range(0, 2);
        }

        if (_activeCoroutine != null)
            return;

        switch (_numberOfBehaviour)
        {
            case 0:
                MoveToRandomY();
                break;

            case 1:
                MoveToPlayerY();
                break;
        }
    }

    private void MoveToShootPoint()
    {
        float minXSpread = 0;
        float maxXSpread = 2;
        Vector2 shootPoint = new Vector2(ShootPoint.position.x - Random.Range(minXSpread, maxXSpread), Randomizer.RandomPositionY());
        _activeCoroutine = StartCoroutine(MoveToPointCoroutine(shootPoint));
        _isShootPointReached = true;
    }

    private void MoveToRandomY()
    {
        Vector3 randomPosition = new Vector2(transform.position.x, Randomizer.RandomPositionY());
        _activeCoroutine = StartCoroutine(MoveToPointCoroutine(randomPosition));
    }

    private void MoveToPlayerY()
    {
        Vector3 playerPositionY = new Vector2(transform.position.x, Target.transform.position.y);
        _activeCoroutine = StartCoroutine(MoveToPointCoroutine(playerPositionY));
    }

    private IEnumerator MoveToPointCoroutine(Vector3 point)
    {
        while (transform.position != point)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * Speed);
            yield return null;
        }

        _activeCoroutine = null;
    }

    private void ResetParam()
    {
        _secondsBetweenChangeBehaviour = Random.Range(_minSecondsBetweenChangeBehaviour, _maxSecondsBetweenChangeBehaviour);
        _timeElapsed = 0;
        _activeCoroutine = null;
        _isShootPointReached = false;
    }
}
