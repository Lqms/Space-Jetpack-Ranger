using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObjectSpawner : Spawner
{
    protected override void Setup(GameObject obj, Vector2 position)
    {
        base.Setup(obj, position);

        if (obj.TryGetComponent(out EnergyObject energyObject))
            energyObject.Setup(SpawnerContainer);
    }
}
