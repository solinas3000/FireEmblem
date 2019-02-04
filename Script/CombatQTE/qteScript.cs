using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qteScript : MonoBehaviour
{

    public Transform cube1;
    public Transform cube2;
    public Transform cube3;
    public Transform cube4;

    public int numberQTE;
    public float minTime;
    public float maxTime;
    public int numberAnim = 4;
    public CombatTextUpdater combatTextUpdater;
    private Dictionary<int, Transform> poolCube;
    private Transform SelectedPrefab;
    private Transform[] listCube;
    private GameObject[] listPositionCube;
    private int numberWin = 0;
    GameObject parentCubePos1;
    GameObject parentCubePos2;
    GameObject parentCubePos3;
    GameObject parentCubePos4;
    private void Start()
    {
        combatTextUpdater = CombatTextUpdater.GetInstance();
        poolCube = new Dictionary<int, Transform>();
        listCube = new Transform[] { cube1, cube2, cube3, cube4 };


        parentCubePos1 = new GameObject();
        parentCubePos1.transform.position = new Vector3(-15, 5, 0);
        parentCubePos1.transform.SetParent(this.transform);
        parentCubePos2 = new GameObject();
        parentCubePos2.transform.position = new Vector3(-5, 5, 0);
        parentCubePos2.transform.SetParent(this.transform);
        parentCubePos3 = new GameObject();
        parentCubePos3.transform.position = new Vector3(5, 5, 0);
        parentCubePos3.transform.SetParent(this.transform);
        parentCubePos4 = new GameObject();
        parentCubePos4.transform.position = new Vector3(15, 5, 0);
        parentCubePos4.transform.SetParent(this.transform);
        listPositionCube = new GameObject[] { parentCubePos1, parentCubePos2, parentCubePos3, parentCubePos4 };

        poolCube.Add(1, cube1);
        poolCube.Add(2, cube2);
        poolCube.Add(3, cube3);
        poolCube.Add(4, cube4);
    }

    public IEnumerator Animate()
    {
        yield return StartCoroutine("AnimateNow");
    }

    IEnumerator AnimateNow()
    {
        numberWin = 0;
        for (int i = 0; i < numberQTE; i++)
        {
            int choice;
            SelectedPrefab = null;
            do
            {
                choice = Random.Range(1, numberAnim + 1);
                if (poolCube.ContainsKey(choice)) SelectedPrefab = poolCube[choice];
            } while (!poolCube.ContainsKey(choice));
            poolCube.Remove(choice);
            Transform cube = Instantiate(listCube[choice - 1]);
            cube.name = "cube " + i;
            cube.gameObject.GetComponent<animCubeScript>().animNumber = choice;
            cube.SetParent(listPositionCube[choice - 1].transform);
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
        yield return finishedQTE();
    }

    public void AddCube(int key)
    {
        poolCube[key] = listCube[key - 1];
    }

    public void IncrementWin()
    {
        Debug.Log("in IncrementWin"); 
        numberWin++;
    }

    private IEnumerator finishedQTE()
    {
        Debug.Log("in finishedQTE qte : " + numberQTE + " win : "+ numberWin);
        if (numberQTE == numberWin)
        {
            showWin();
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(LoadCombatScene.GetInstance().putDamage());
        }  
        else
        {
            showLoose();
            yield return new WaitForSeconds(1);
        }
        
    }

    public void showTurn(string name)
    {
        combatTextUpdater.UpdateValue(name + " be ready !");
    }

    private void showWin()
    {
        combatTextUpdater.UpdateValue("You win !");
    }

    private void showLoose()
    {
        combatTextUpdater.UpdateValue("You loose !");
    }

    public void showDead(string name)
    {
        combatTextUpdater.UpdateValue(name+ ", you are dead !");
    }

    public void showEnd()
    {
        combatTextUpdater.UpdateValue("End of Combat !");
    }
}
