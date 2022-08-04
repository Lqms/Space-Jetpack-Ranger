using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 25;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TouchScreen _touchScreen;

    private void OnEnable()
    {
        _touchScreen.AttackButtonPressed += Shoot;
    }

    private void OnDisable()
    {
        _touchScreen.AttackButtonPressed -= Shoot;
    }

    public void Shoot()
    {
        _currentWeapon.Shoot(_shootPoint, Vector2.right, _damage);
    }
} 
