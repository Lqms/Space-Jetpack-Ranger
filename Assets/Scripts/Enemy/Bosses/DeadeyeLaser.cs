using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadeyeLaser : MonoBehaviour
{
    [SerializeField] private AudioClip _prepare;
    [SerializeField] private ParticleSystem _laserEffect;
    [SerializeField] private ParticleSystem _laserTrails;
    [SerializeField] private float _baseDamage = 100;
    [SerializeField] private Deadeye _deadeye;
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _deadeye.Shooted += OnShooted;
        _deadeye.StartShooting += OnStartShooting;
    }

    private void OnDisable()
    {
        _deadeye.Shooted -= OnShooted;
        _deadeye.StartShooting -= OnStartShooting;
    }

    private void OnShooted()
    {
        _laserEffect.Stop();
        _laserTrails.Stop();
        _weapon.Shoot(transform, Vector2.left, _baseDamage + LevelManager.CurrentWave * LevelManager.BossBonusDamagePerLevel);
    }

    private void OnStartShooting()
    {
        AudioManager.Instance.PlayClip(_prepare);
        _laserTrails.Play();
        _laserEffect.Play();
    }
}
