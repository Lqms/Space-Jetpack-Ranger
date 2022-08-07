using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartAnimator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _destinationPoint;
    [SerializeField] private Transform _startPoint;

    private float _distance;

    public event UnityAction<float> Moved;
    public event UnityAction AnimationFinished;

    private void Start()
    {
        transform.position = _startPoint.position;
        _distance = Vector2.Distance(_startPoint.position, _destinationPoint.position);
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (transform.position.x < _destinationPoint.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, _destinationPoint.position, _speed * Time.deltaTime);
            float distanceXTraveled = Vector2.Distance(_startPoint.position, transform.position);
            float distanceProgress = distanceXTraveled / _distance;
            Moved?.Invoke(distanceProgress);
            yield return null;
        }

        AnimationFinished?.Invoke();
    }
}
