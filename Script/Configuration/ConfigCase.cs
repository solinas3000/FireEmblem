using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCase : ScriptableObject
{
    public object SetupTerrain(EnumTerrain.TerrainEnum ter)
    {
        switch (ter)
        {
            case EnumTerrain.TerrainEnum.Forest:
                return new Forest();
            case EnumTerrain.TerrainEnum.Mountain:
                return new Mountain();
            case EnumTerrain.TerrainEnum.Plain:
                return new Plain();
            case EnumTerrain.TerrainEnum.Sea:
                return new Sea();
            case EnumTerrain.TerrainEnum.Structure:
                return new Structure();
            default:
                return null;
        }
    }
}
