using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : ITerrainType
{
    public double getBonusTerrain()
    {
        return 1;
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
