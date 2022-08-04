using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot(Transform shootPoint, Vector2 direction, float damage)
    {
        Bullet bullet = Instantiate(Bullet, shootPoint);
        bullet.Setup(direction, damage);
        AudioManager.Instance.PlayClip(ShotClip);
    }
}
