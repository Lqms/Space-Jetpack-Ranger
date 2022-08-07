using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeathBarrierActivator : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = false;
        _player.Energy.RunOut += OnPlayerEnergyRunOut;
        _player.Energy.Restored += OnPlayerEnergyRestored;
    }

    private void OnDisable()
    {
        _player.Energy.RunOut -= OnPlayerEnergyRunOut;
        _player.Energy.Restored -= OnPlayerEnergyRestored;
    }

    private void OnPlayerEnergyRunOut()
    {
        _collider.isTrigger = true;
    }

    private void OnPlayerEnergyRestored()
    {
        _collider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Health.ApplyDamage(player.Health.Max);
        }
    }
}
