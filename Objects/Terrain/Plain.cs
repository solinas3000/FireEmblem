using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain : ITerrainType
{
    public double getBonusTerrain()
    {
        return 1;
    }

    public int getMovementPenaly(IMovementType movementType)
    {
        return 1;
    }
}
