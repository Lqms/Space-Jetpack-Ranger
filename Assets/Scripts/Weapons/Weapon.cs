using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet Bullet;
    [SerializeField] protected AudioClip ShotClip;

    public abstract void Shoot(Transform shootPoint, Vector2 direction, float damage);
}
