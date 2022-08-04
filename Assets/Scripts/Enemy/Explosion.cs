using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage + LevelManager.CurrentLevel * LevelManager.BonusDamagePerLevel);
        }
    }

    /// <summary>
    /// calls in game object's animation
    /// </summary>
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
