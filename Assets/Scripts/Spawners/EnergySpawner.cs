using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpawner : Spawner
{
    public void SpawnEnergy(Vector2 position)
    {
        if (TryGetObject(out GameObject energy))
        {
            Setup(energy, position);
        }
    }
}
