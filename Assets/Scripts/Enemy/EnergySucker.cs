using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class EnergySucker : Enemy
{
    [SerializeField] private Vector3[] _wayPoints;
    [SerializeField] private float _patrolTime = 5;
    [SerializeField] private float _fadeTime = 3;
    [SerializeField] private float _showTime = 1;
    [SerializeField] private float _percentOfStealingEnergy = 10;
    [SerializeField] private Color _fadedColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _totalStolenEnergy;
    private Coroutine _activeCoroutine;

    protected override void OnEnable()
    {
        base.OnEnable();
        _totalStolenEnergy = 0;
        _activeCoroutine = StartCoroutine(FadeCoroutine());
        Tween moveTween = transform.DOPath(_wayPoints, _patrolTime / Speed, PathType.Linear).SetOptions(true).SetLoops(-1);
    }

    protected override void OnDied()
    {
        StopCoroutine(_activeCoroutine);
        Target.Energy.Restore(_totalStolenEnergy);

        base.OnDied();
    }

    private IEnumerator FadeCoroutine()
    {
        while (true)
        {
            _spriteRenderer.color = _fadedColor;

            yield return new WaitForSeconds(_fadeTime);

            _spriteRenderer.color = _normalColor;
            float normalSpeed = Speed;
            float amountOfStealingEnergy = Target.Energy.Current * (_percentOfStealingEnergy / 100);
            _totalStolenEnergy += amountOfStealingEnergy;
            Speed = 0;
            Target.Energy.Decrease(amountOfStealingEnergy);
            Target.Energy.StealEnergyEffect.Play();

            yield return new WaitForSeconds(_showTime);
            Speed = normalSpeed;
        }
    }
}
