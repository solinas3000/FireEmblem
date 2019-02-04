using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class GridGenerator : EditorWindow
{


    public int gridWidth = 11;
    public int gridHeight = 11;

    
    public float refinementTerrain = 0f;
    public string gridName;
    
    private float hexWidth = 1.732f;
    private float hexHeight = 2.0f;
    private float gap = 0.0f;
    private Vector3 startPos;
    private CreateCase createCase;
    private GameObject grid;
    private string oldGridName;
    private float perlinNoiseTerrain = 0f;

    [MenuItem("Window/Grid Generator")]
    static void Init()
    {
        GridGenerator window = (GridGenerator)EditorWindow.GetWindow(typeof(GridGenerator));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        gridName = EditorGUILayout.TextField("Name", gridName);

        gridWidth = EditorGUILayout.IntField("gridWidth", gridWidth);
        gridHeight = EditorGUILayout.IntField("gridHeight", gridHeight);

        refinementTerrain = EditorGUILayout.Slider("refinementTerrain", refinementTerrain, 0, 3);
        if (GUILayout.Button("generate"))
        {
            GameObject oldGrid = GameObject.Find(oldGridName);
            if(oldGrid != null)
            {
                GameObject.Destroy(oldGrid);
            }
            createCase = ScriptableObject.CreateInstance<CreateCase>();
            AddGap();
            CalcStartPos();
            CreateGrid();
        }

    }

    void CreateGrid()
    {
        grid = new GameObject();
        grid.AddComponent<GridScript>();
        grid.GetComponent<GridScript>().gridHeight = gridHeight;
        grid.GetComponent<GridScript>().gridWidth = gridWidth;
        grid.transform.name = gridName;
        oldGridName = gridName;
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject hexPosObj;
                Transform hex;
                Vector2 gridPos = new Vector2(x, y);
                perlinNoiseTerrain = Mathf.PerlinNoise(gridPos.x * refinementTerrain, gridPos.y * refinementTerrain);
                hexPosObj = new GameObject();
                hexPosObj.transform.position = CalcWorldPos(gridPos);
                hexPosObj.transform.SetParent(grid.transform, false);
                hexPosObj.transform.name = "HexPos " + x + "|" + y;
                if (perlinNoiseTerrain > 0.35)
                {
                    float randValue = UnityEngine.Random.value;
                    if (randValue < .95f)
                    {
                        hex = createCase.CreatePrefab(EnumTerrain.TerrainEnum.Plain, gridPos);
                    }
                    else
                    {
                        hex = createCase.CreatePrefab(EnumTerrain.TerrainEnum.Structure, gridPos);
                    }
                }
                else
                {
                    hex = createCase.CreatePrefab(EnumTerrain.TerrainEnum.Sea, gridPos);
                }
                hex.SetParent(hexPosObj.transform, false);
            }
        }
        grid.GetComponent<GridScript>().selectorPrefab = Resources.Load("CaseSelection/CaseSelection2", typeof(Transform)) as Transform;
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);
        startPos = new Vector3(x, 0, z);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }
}