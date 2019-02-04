using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrainType
{
    double getBonusTerrain();
    int getMovementPenaly(IMovementType movementType);
}
