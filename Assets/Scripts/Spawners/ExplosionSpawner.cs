using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : Spawner
{
    public void SpawnExplosion(Vector2 position)
    {
        if (TryGetObject(out GameObject explosion))
        {
            Setup(explosion, position);
        }
    }
}
