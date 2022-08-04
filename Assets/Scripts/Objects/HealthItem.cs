using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private float _amountOfRestoredHealth;
    private float _reduceCoefficient = 10;

    public float AmountOfRestoredHealth => _amountOfRestoredHealth;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    public void Setup(float amountOfRestoredHealth)
    {
        _amountOfRestoredHealth = amountOfRestoredHealth / _reduceCoefficient;
    }
}
