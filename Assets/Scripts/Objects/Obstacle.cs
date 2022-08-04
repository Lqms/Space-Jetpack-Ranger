using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _damage = 25;
    [SerializeField] private Health _health;

    protected SpawnerContainer SpawnerContainer;
    protected bool DestroyedByPlayer = true;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            DestroyedByPlayer = false;
            player.ApplyDamage(_damage);
            _health.ApplyDamage(_health.Max);
        }
    }

    protected virtual void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.Died += OnDied;
        DestroyedByPlayer = true;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    protected virtual void OnDied()
    {
        gameObject.SetActive(false);
    }
}
