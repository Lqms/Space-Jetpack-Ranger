using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyUISpawner : Spawner
{
    public BountyUI Bounty { get; private set; }

    protected override void Setup(GameObject bounty, Vector2 position)
    {
        base.Setup(bounty, position);

        if (bounty.TryGetComponent(out BountyUI bountyScript))
            Bounty = bountyScript;
    }
}
