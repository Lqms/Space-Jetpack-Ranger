using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float Speed = 1;
    [SerializeField] protected float Damage = 25;
    [SerializeField] protected Health Health;

    private void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(Damage);
            Health.ApplyDamage(Health.Max);
        }
    }

    protected virtual void OnEnable()
    {
        Health = GetComponent<Health>();
        Health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        Health.Died -= OnDied;
    }

    protected virtual void OnDied()
    {
        gameObject.SetActive(false);
    }
}
