using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateCase : ScriptableObject
{
    private Dictionary<EnumTerrain.TerrainEnum, Transform[]> CasePrefab = new Dictionary<EnumTerrain.TerrainEnum, Transform[]>();
    ITerrainType classTerrain;
    public Transform CreatePrefab(EnumTerrain.TerrainEnum terrain, Vector2 gridPos)
    {
        if (!CasePrefab.ContainsKey(terrain))
        {
            Transform[] transfos = Resources.LoadAll("CasePrefab/" + terrain.ToString(), typeof(Transform))
                .Cast<Transform>()
                .ToArray();
            if (transfos == null)
            {
                Debug.Log("tried to instantates prefabs " + "CasePrefab/" + terrain.ToString() + ". but they does not exist");
                return null;
            }
            CasePrefab.Add(terrain, transfos);
        }
        int rndCase = UnityEngine.Random.Range(0, CasePrefab[terrain].Length);
        Transform caseConfigured =  Instantiate(CasePrefab[terrain][rndCase]) as Transform;
        caseConfigured.gameObject.AddComponent(Type.GetType("CaseScript"));
        
        if(!(terrain == EnumTerrain.TerrainEnum.Sea))
        {
            float perlinNoiseHeigth = Mathf.PerlinNoise((float)(gridPos.x * 0.1), (float)(gridPos.y * 0.1));
            caseConfigured.localPosition = new Vector3(0, perlinNoiseHeigth, 0);
            caseConfigured.localScale = new Vector3(1, 1, perlinNoiseHeigth);
        }
        
        caseConfigured.gameObject.GetComponent<CaseScript>().terEnum = terrain;
        caseConfigured.gameObject.AddComponent<MeshCollider>();
        caseConfigured.gameObject.GetComponent<MeshCollider>().convex = true;
        caseConfigured.gameObject.GetComponent<MeshCollider>().isTrigger = true;

        caseConfigured.gameObject.GetComponent<CaseScript>().x = (int)gridPos.x;
        caseConfigured.gameObject.GetComponent<CaseScript>().y = (int)gridPos.y;
        return caseConfigured;
    }

}
