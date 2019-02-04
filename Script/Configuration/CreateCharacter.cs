using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateCharacter: ScriptableObject
{
    private Dictionary<EnumMouvement.MouvementEnum, Transform[]> CasePrefab = new Dictionary<EnumMouvement.MouvementEnum, Transform[]>();
    IMovementType classMouvement;
    IWeaponType classWeapon;
    public Transform CreatePrefab(EnumMouvement.MouvementEnum mvt, EnumWeapon.WeaponEnum weapon, TeamScript team)
    {
        if (!CasePrefab.ContainsKey(mvt))
        {
            Transform[] transfos = Resources.LoadAll("Character/" + mvt.ToString(), typeof(Transform))
                .Cast<Transform>()
                .ToArray();
            if (transfos == null)
            {
                Debug.Log("tried to instantates prefabs " + "Character/" + mvt.ToString() + ". but they does not exist");
                return null;
            }
            CasePrefab.Add(mvt, transfos);
        }
        int rndCase = UnityEngine.Random.Range(0, CasePrefab[mvt].Length);
        Transform characterConfigured = Instantiate(CasePrefab[mvt][rndCase]) as Transform;
        characterConfigured.gameObject.AddComponent(Type.GetType("CharacterScript"));
        characterConfigured.gameObject.GetComponent<CharacterScript>().enumWeapon = weapon;
        characterConfigured.gameObject.GetComponent<CharacterScript>().enumMouvement = mvt;
        characterConfigured.gameObject.GetComponent<CharacterScript>().team = team;
        characterConfigured.gameObject.AddComponent<MeshCollider>();
        characterConfigured.gameObject.GetComponent<MeshCollider>().convex = true;
        characterConfigured.gameObject.GetComponent<MeshCollider>().isTrigger = true;
        if (team.teamNumber == 1) characterConfigured.gameObject.GetComponent<Renderer>().material.color = Color.black;
        if (team.teamNumber == 2) characterConfigured.gameObject.GetComponent<Renderer>().material.color = Color.white;
        return characterConfigured;
    }

}
