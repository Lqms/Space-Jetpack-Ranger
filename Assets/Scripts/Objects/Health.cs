using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _base;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _death;
    [SerializeField] private float _current;
    [SerializeField] private float _max;

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
            AudioManager.Instance.PlayClip(_hit);
            Damaged?.Invoke();
        }
    }

    public void IncreaseMaxHealth(float amountOfIncrease)
    {
        _max = _base + amountOfIncrease;
    }

    public void Heal(float amountOfHeal)
    {
        _current = Mathf.Clamp(_current + amountOfHeal, 0, _max);
    }
}
