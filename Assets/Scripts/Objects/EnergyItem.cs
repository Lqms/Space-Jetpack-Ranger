using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    [SerializeField] private float _amountOfRestoredEnergy = 25;
    [SerializeField] private float _speed = 1;

    public float AmountOfRestoredEnergy => _amountOfRestoredEnergy;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
