using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : Spawner
{
    [SerializeField] private EnergySpawner _energySpawner;

    protected override void Setup(GameObject obj, Vector2 position)
    {
        base.Setup(obj, position);

        if (obj.TryGetComponent(out EnergyObject energyObject))
            energyObject.Initialize(_energySpawner);
    }
}
