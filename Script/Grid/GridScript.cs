using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public Transform selectorPrefab;
    public CameraScript CamScript;
    public int gridWidth;
    public int gridHeight;
    private bool selected = false;
    public Hashtable selectableHexPos;
    [SerializeField]
    public Transform[,] matrix;
    Stack selectors;
    public TextUpdater textUpdater;

    //private GameObject lastClickedObject;
    public Transform lastClickedObject { get; set; }
    //public bool clickedCase { get; set; }
    //public bool engageEnnemy { get; set; }

    //TEMPORAIRE
    public enum GameState
    {
        Start,
        Move,
        Engage
    }
    public GameState state;
    //TEMPORAIRE

    public List<Transform> listEnnemy;

    void Awake()
    {
        matrix = new Transform[gridWidth, gridHeight];
        int compteur = 0;
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                matrix[x, y] = this.transform.GetChild(compteur);
                compteur++;
            }
        }
    }

    void Start()
    {
        CamScript = CameraScript.GetInstance();
        textUpdater = TextUpdater.GetInstance();
        selectableHexPos = new Hashtable();
        selectors = new Stack();
        lastClickedObject = null;
        //clickedCase = false;
        //engageEnnemy = false;

        //TEMPORAIRE
        state = GameState.Start;
        //TEMPORAIRE
    }

    public void SelectCases(int x, int y, int numberMouvement, IMovementType movementType)
    {
        if (numberMouvement < 0) return;
        if (x < 0 || x >= gridWidth) return;
        if (y < 0 || y >= gridHeight) return;
        Transform hexPos = matrix[x, y];
        int MovementPenalty = hexPos.GetChild(0).GetComponent<CaseScript>().terrain.getMovementPenaly(movementType);
        if (MovementPenalty == -1) return;
        if (numberMouvement != movementType.GetMovementNumber() && hexPos.Find("Character")) return;
        if (!selectableHexPos.ContainsKey(hexPos.GetHashCode()))
        {
            AttachSelector(hexPos);
        }
        SelectCases(x - 1, y, numberMouvement - MovementPenalty, movementType);
        SelectCases(x + 1, y, numberMouvement - MovementPenalty, movementType);
        SelectCases(x, y + 1, numberMouvement - MovementPenalty, movementType);
        SelectCases(x, y - 1, numberMouvement - MovementPenalty, movementType);
        if (y % 2 == 0)
        {
            SelectCases(x - 1, y - 1, numberMouvement - MovementPenalty, movementType);
            SelectCases(x - 1, y + 1, numberMouvement - MovementPenalty, movementType);
        }
        else
        {
            SelectCases(x + 1, y - 1, numberMouvement - MovementPenalty, movementType);
            SelectCases(x + 1, y + 1, numberMouvement - MovementPenalty, movementType);
        }

    }

    public void DestroySelectCases()
    {
        Transform selector;
        try
        {
            while (selector = selectors.Pop() as Transform)
            {
                Poolable.Destroy(selector.gameObject);
            }
        }
        catch (InvalidOperationException) { };
        selectableHexPos.Clear();
    }

    public void AttachSelector(Transform hexPos)
    {
        Transform selector = Poolable.Instantiate(selectorPrefab.gameObject, Vector3.zero, Quaternion.identity).transform;//Instantiate(selectorPrefab) as Transform;
        selector.localPosition = new Vector3(0, (float)(hexPos.GetChild(0).localScale.z * 2 + 0.1), 0);
        selectors.Push(selector);
        selector.SetParent(hexPos, false);
        selectableHexPos.Add(hexPos.GetHashCode(), hexPos);

    }

    public void findEnnemy(int x, int y, TeamScript team, List<Transform> toReturn)
    {
        Transform character = matrix[x, y].Find("Character");
        //Debug.Log(character.gameObject.GetComponent<CharacterScript>().team.teamNumber);
        if (character != null && character.gameObject.GetComponent<CharacterScript>().team.teamNumber != team.teamNumber) toReturn.Add(character);
    }

    public void HexPosWithEnnemies(int x, int y, TeamScript team){
        List<Transform> toReturn = new List<Transform>();
        //X+1 Y
        if (x+1 >= 0 && x+1 < gridWidth && y >= 0 && y < gridHeight)
        {
            findEnnemy(x + 1, y, team, toReturn);
        }
        //X Y+1
        if (x >= 0 && x < gridWidth && y+1 >= 0 && y+1 < gridHeight)
        {
            findEnnemy(x, y + 1, team, toReturn);
        }
        //X-1 Y
        if (x-1 >= 0 && x-1 < gridWidth - 1 && y >= 0 && y < gridHeight)
        {
            findEnnemy(x - 1, y, team, toReturn);
        }
        //X Y-1
        if (x >= 0 && x < gridWidth && y-1 >= 0 && y-1 < gridHeight)
        {
            findEnnemy(x, y-1, team, toReturn);
        }
        //Cases diagonales
        if (y % 2 == 0)
        {
            //X-1 Y-1
            if (x-1 >= 0 && x-1 < gridWidth && y-1 >= 0 && y-1 < gridHeight)
            {
                findEnnemy(x - 1, y - 1, team, toReturn);
            }
            //X-1 Y+1
            if (x-1 >= 0 && x-1 < gridWidth && y+1 >= 0 && y+1 < gridHeight)
            {
                findEnnemy(x - 1, y + 1, team, toReturn);
            }
        }
        else
        {
            //X+1 Y-1
            if (x+1 >= 0 && x+1 < gridWidth && y-1 >= 0 && y-1 < gridHeight)
            {
                findEnnemy(x + 1, y - 1, team, toReturn);
            }
            //X+1 Y+1
            if (x+1 >= 0 && x+1 < gridWidth && y+1 >= 0 && y+1 < gridHeight)
            {
                findEnnemy(x + 1, y + 1, team, toReturn);
            }
        }
        listEnnemy = toReturn;
    }
}