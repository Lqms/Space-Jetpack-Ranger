using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private bool _isDestroyingOnHit;

    private float _damage;
    private Vector2 _direction;

    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.ApplyDamage(_damage);

            if (_isDestroyingOnHit)
                Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 direction, float damage)
    {
        _direction = direction;
        _damage = damage;
    }
}
