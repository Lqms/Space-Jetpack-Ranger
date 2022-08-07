using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceJuggernautMover))]
public class SpaceJuggernautShooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootSpreadY = 2;

    private SpaceJuggernautMover _mover;
    private float _timeElapsed;

    private void OnEnable()
    {
        _mover = GetComponent<SpaceJuggernautMover>();
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _reloadTime)
        {
            if (transform.position.y >= _mover.Target.transform.position.y - _shootSpreadY && 
                transform.position.y <= _mover.Target.transform.position.y + _shootSpreadY)
            {
                _weapon.Shoot(_shootPoint, Vector2.left, _damage + LevelManager.CurrentWave * LevelManager.BonusDamagePerLevel);
                _timeElapsed = 0;
            }
        }
    }
}
