using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class TeamGenerator : EditorWindow
{
    int teamNumber = -1;
    int typeCharacter = -1;
    int typeWeapon = -1;
    bool generatedGameTeam = false;
    bool selectionDone = false;
    GameObject[] selectedCases;
    GameObject gameTeam;
    GameObject team1;
    GameObject team2;
    private CreateCharacter createCharacter = null;
    private string[] listMouvementString = Enum.GetValues(typeof(EnumMouvement.MouvementEnum))
                                        .Cast<EnumMouvement.MouvementEnum>()
                                        .Select(v => v.ToString())
                                        .ToArray();

    private int[] listMouvementInt = Enum.GetValues(typeof(EnumMouvement.MouvementEnum))
                                        .Cast<int>()
                                        .ToArray();

    private string[] listWeaponString = Enum.GetValues(typeof(EnumWeapon.WeaponEnum))
                                    .Cast<EnumWeapon.WeaponEnum>()
                                    .Select(v => v.ToString())
                                    .ToArray();

    private int[] listWeaponInt = Enum.GetValues(typeof(EnumWeapon.WeaponEnum))
                                        .Cast<int>()
                                        .ToArray();

    [MenuItem("Window/Team Generator")]
    static void Init()
    {
        TeamGenerator window = (TeamGenerator)EditorWindow.GetWindow(typeof(TeamGenerator));
        window.Show();
    }

    void OnSelectionChange()
    {
        selectedCases = Selection.gameObjects;
        if (selectedCases.Length > 0) {
            selectionDone = true;
        }
        else
        {
            selectionDone = false;
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        if (!generatedGameTeam ? GUILayout.Button("generate GameTeam") : false)
        {
            InitGameTeam();
            generatedGameTeam = true;
        }
        if (generatedGameTeam)
        {
            GUILayout.Label("Character Settings", EditorStyles.boldLabel);
            teamNumber = EditorGUILayout.IntPopup("Select team number", teamNumber, new string[] { "team 1", "team 2" }, new int[] { 1, 2 });
            typeCharacter = EditorGUILayout.IntPopup("Select type Character", typeCharacter, listMouvementString, listMouvementInt);
            typeWeapon = EditorGUILayout.IntPopup("Select type Weapon", typeWeapon, listWeaponString, listWeaponInt);
        }
        if (teamNumber >= 0 && typeCharacter >= 0 && typeWeapon >= 0)
        {
            GUILayout.Label("Please select one or more case(s) in the editor", EditorStyles.boldLabel);
            if(selectionDone)
            {
                if(GUILayout.Button("attach character to case(s)"))
                {
                    createCharacter = ScriptableObject.CreateInstance<CreateCharacter>();
                    AttachCharacters();
                }
            }
        }
    }

    private void InitGameTeam()
    {
        gameTeam = new GameObject();
        gameTeam.AddComponent<GameTeamScript>();
        gameTeam.name = "gameTeam";
        team1 = new GameObject();
        team1.AddComponent<TeamScript>();
        team1.name = "team1";
        team1.GetComponent<TeamScript>().teamNumber = 1;
        team1.transform.SetParent(gameTeam.transform);

        team2 = new GameObject();
        team2.AddComponent<TeamScript>();
        team2.name = "team2";
        team2.GetComponent<TeamScript>().teamNumber = 2;
        team2.transform.SetParent(gameTeam.transform);
    }

    private void AttachCharacters()
    {
        foreach(GameObject c in selectedCases)
        {
            TeamScript tS = null;
            if (teamNumber == 1) tS = team1.GetComponent<TeamScript>();
            if (teamNumber == 2) tS = team2.GetComponent<TeamScript>();
            Transform character = createCharacter.CreatePrefab((EnumMouvement.MouvementEnum)typeCharacter,
                                                                        (EnumWeapon.WeaponEnum)typeWeapon,
                                                                        tS);
            Debug.Log(c.transform.parent.localScale);
            character.localPosition = new Vector3(0, (float)0.6+(c.transform.parent.GetChild(0).localScale.z*2), 0);
            character.SetParent(c.transform.parent, false);
            character.name = "Character";
            tS.characters.Add(character);
        }
    }
}
