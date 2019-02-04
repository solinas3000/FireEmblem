using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScript : MonoBehaviour
{
    public EnumTerrain.TerrainEnum terEnum;
    public ITerrainType terrain;
    public int x;
    public int y;
    private bool isClicked = false;

    void Awake()
    {
        ConfigCase c = ScriptableObject.CreateInstance<ConfigCase>();
        terrain = (ITerrainType)c.SetupTerrain(terEnum);
    }

    public void OnMouseDown()
    {
        //TEMPORAIRE
        //La grille du jeu
        GridScript grid = gameObject.transform.parent.parent.GetComponent<GridScript>();
        GridScript.GameState state = grid.state;

        //Regarde la team du joueur
        int currentTeamNumber = GameTeamScript.GetInstance().currentTeamNumber;

        //Récupère éventuellement le personnage sur la case choisie, son traitement
        //varie en fonction de l'état du jeu
        Transform character = gameObject.transform.parent.Find("Character");

        switch (state)
        {
            //Etat start :  le joueur n'a pas encore cliqué sur un personnage
            case GridScript.GameState.Start:
                //Si la case contient un joueur
                if (character != null)
                {
                    //Check si c'est son tour, si non, on l'ignore
                    if (currentTeamNumber != character.gameObject.GetComponent<CharacterScript>().team.teamNumber) return;
                    //Change l'état du jeu
                    grid.state = GridScript.GameState.Move;
                    //Sauvegarde l'unité clické
                    grid.lastClickedObject = character;
                    //On affiche les case où le joueur peut aller
                    grid.SelectCases(x, y, character.GetComponent<CharacterScript>().GetMovementNumber(), character.GetComponent<CharacterScript>().movementType);
                    //grid.cam.setCameraTarget(character.position);
                    grid.CamScript.setCameraTarget(character.parent.position, character.GetComponent<CharacterScript>().team.teamNumber);
                }
                break;
            
            //Etat move :   le joueur a cliqué sur un personnage, il doit choisir où se déplacer
            case GridScript.GameState.Move:
                //On regarde si le joueur clique sur une case où il peut se déplacer
                if (grid.selectableHexPos.ContainsValue(this.transform.parent))
                {
                    //Effectue le déplacement
                    //Met l'unité sur la position de la case
                    grid.lastClickedObject.localPosition = new Vector3(0, (float)(this.transform.localScale.z * 2 + 0.6), 0);
                    //Set le parent
                    grid.lastClickedObject.SetParent(this.transform.parent, false);
                    //Détruit le path
                    grid.DestroySelectCases();

                    //Lancer l'interraction si jamais à côté d'un ennemi
                    grid.HexPosWithEnnemies(x, y, grid.lastClickedObject.gameObject.GetComponent<CharacterScript>().team);

                    //Lancer l'animation d'engage. Si il y a des ennemis, on change d'état
                    if (grid.listEnnemy.Count > 0)
                    {
                        //Change l'état du jeu
                        grid.state = GridScript.GameState.Engage;
                        //Rajoute l'animation du selecteur
                        foreach (Transform t in grid.listEnnemy)
                        {
                            grid.AttachSelector(t.parent);
                        }
                        grid.AttachSelector(this.transform.parent);
                        return;
                    }

                    //Sinon, on repasse à l'état par défaut
                    //Change l'état du jeu
                    grid.state = GridScript.GameState.Start;
                    //Supprime l'unité clické
                    grid.lastClickedObject = null;
                    GameTeamScript.GetInstance().ChangeTurn();


                }
                //Sinon, le joueur ne clique pas sur une case où il peut se déplacer
                else
                {
                    //Reset la map
                    //Change l'état du jeu
                    grid.state = GridScript.GameState.Start;
                    //Supprime l'unité clické
                    grid.lastClickedObject = null;
                    //Détruit le path
                    grid.DestroySelectCases();

                    //A MIEUX GERER
                    grid.textUpdater.UpdateUnitValue(null);
                }
                break;

            //Etat engage : le joueur a déplacé son personnage à proximité d'un adversaire,
            //              à lui de choisir si oui ou non il engage le combat
            case GridScript.GameState.Engage:

                //Si le joueur clique sur sa case, il passe son tour
                if (grid.lastClickedObject.Equals(character) || grid.listEnnemy.Contains(character))
                {
                    //Si c'est un ennemie, on change de scene
                    if (grid.listEnnemy.Contains(character))
                    {
                        //LOAD ICI LA SCENE
                        Debug.Log("LOAD SCENE COMBAT");
                        LoadCombatScene loadCombat = LoadCombatScene.GetInstance();
                        loadCombat.CombatScene(grid.lastClickedObject, character);
                    }
                    else
                    {
                        GameTeamScript.GetInstance().ChangeTurn();
                    }

                    //Tout reset
                    grid.state = GridScript.GameState.Start;
                    grid.lastClickedObject = null;
                    grid.DestroySelectCases();
                }
                //Ne fais rien tant que le joueur ne clique pas sur une case contenant un joueur
                break;
        }
        //TEMPORAIRE

    }
}
