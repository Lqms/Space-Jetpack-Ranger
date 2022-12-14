using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _base;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _death;
    [SerializeField] private float _max;
    [SerializeField] private float _current;

    public float Max => _max;
    public float Current => _current;
    public float Base => _base;

    public event UnityAction Died;
    public event UnityAction Damaged;

    private void OnEnable()
    {
        _current = _max;
    }

    public void ApplyDamage(float damage)
    {
        _current -= damage;

        if (_current <= 0)
        {
            Died?.Invoke();
            AudioManager.Instance.PlayClip(_death);
        }
        else
        {
            Damaged?.Invoke();
            AudioManager.Instance.PlayClip(_hit);
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        _max = _base + amount;
    }

    public void Restore(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0, _max);
    }

    public void SetHealth(int health)
    {
        _max = health;
        _current = _max;
    }
}
