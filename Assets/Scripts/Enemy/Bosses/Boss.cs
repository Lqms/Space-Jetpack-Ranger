using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float Speed = 2;

    [SerializeField] private int _baseReward;
    [SerializeField] private Health _bossHealth;

    public event UnityAction ShootPointReached;

    private void OnEnable()
    {
        _bossHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _bossHealth.Died -= OnDied;
    }

    private void OnDied()
    {
        PlayerManager.Instance.IncreaseMoney(_baseReward * LevelManager.CurrentWave);
        Destroy(gameObject);
        DifficultyManager.Instance.LevelUp();
        PlayerManager.Instance.TrySaveWave(true);
        PlayerManager.Instance.TrySaveMoney(true);
    }

    public void MoveToPoint(Transform point)
    {
        StartCoroutine(MovingToPoint(point));
    }

    private IEnumerator MovingToPoint(Transform point)
    {
        while (transform.position != point.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, Speed * Time.deltaTime);
            yield return null;
        }

        ShootPointReached?.Invoke();
    }
}
