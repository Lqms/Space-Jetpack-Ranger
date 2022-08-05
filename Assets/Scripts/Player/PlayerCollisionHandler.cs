using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnergyItem energy))
        {
            _player.RestoreEnergy();
            energy.gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out HealthItem health))
        {
            _player.RestoreHealth(25);
            health.gameObject.SetActive(false);
        }
    }
}
