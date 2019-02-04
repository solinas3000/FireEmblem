using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : ITerrainType
{
    public double getBonusTerrain()
    {
        return 1.20;
    }

    public int getMovementPenaly(IMovementType movementType)
    {
        switch (movementType.GetType().ToString())
        {
            case "Cavalry":
                return 3;
            case "Flyier":
                return 1;
            default:
                return 2;
        };
    }
}
