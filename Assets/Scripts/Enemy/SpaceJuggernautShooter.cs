using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJuggernautShooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _shootPoint;

    private float _timeElapsed;

    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _reloadTime)
        {
            _weapon.Shoot(_shootPoint, Vector2.left, _damage + LevelManager.CurrentLevel * LevelManager.BonusDamagePerLevel);
            _timeElapsed = 0;
        }
    }
}
