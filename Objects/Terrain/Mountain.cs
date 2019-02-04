using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : ITerrainType
{
    public double getBonusTerrain()
    {
        return 1.20;
    }

    public int getMovementPenaly(IMovementType movementType)
    {
        switch (movementType.GetType().ToString())
        {
            case "Flyier":
                return 1;
            default:
                return -1;
        };
    }
}
