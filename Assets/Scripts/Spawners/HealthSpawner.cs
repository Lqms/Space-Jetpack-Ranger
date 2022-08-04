using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : Spawner
{
    public void SpawnHealth(Vector2 position, float amountOfRestoredHealth)
    {
        if (TryGetObject(out GameObject health))
        {
            Setup(health, position);

            if (health.TryGetComponent(out HealthItem healthItem))
            {
                healthItem.Setup(amountOfRestoredHealth);
            }
        }
    }
}
