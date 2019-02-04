using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCombatScene : AbstractSingleton<LoadCombatScene>
{
	GameObject qte;
    CharacterScript currentEnnemy;
    CharacterScript currentAlly;
    bool finishedCombat = false;

    public void CombatScene(Transform char1, Transform char2)
    {
        StartCoroutine(LoadAsyncScene(char1, char2));
    }

    IEnumerator LoadAsyncScene(Transform char1, Transform char2)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject temp = new GameObject("stageTempGO");
        GameObject[] allObjects = currentScene.GetRootGameObjects();
        foreach(GameObject go in allObjects) {

            if(go != GameObject.Find("LoadCombatScene") && go != GameObject.Find("gameTeam")) go.transform.SetParent(temp.transform, false);

        }
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("combat", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        
        //UnShow the previous Scene and add character to the new scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("combat"));
        Transform ct1 = Instantiate(char1, new Vector3(-10, -5, 10), Quaternion.identity);
        Transform ct2 = Instantiate(char2, new Vector3(10, -5, 10), Quaternion.identity);
        ct1.localScale = new Vector3(3, 3, 3);
        ct2.localScale = new Vector3(3, 3, 3);
        temp.SetActive(false);

        //engageCombat
        qte = GameObject.Find("QTE");
		CharacterScript c1 = char1.gameObject.GetComponent<CharacterScript>();
		CharacterScript c2 = char2.gameObject.GetComponent<CharacterScript>();
        yield return engageCombat(c1,c2); // wait until the end of combat

		//DestroyCombatScene and Reload GridScene;

		AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("combat");
		while (!asyncUnload.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
        temp.transform.DetachChildren();
        GameTeamScript.GetInstance().ChangeTurn();
        Destroy(temp);
    }

	public IEnumerator putDamage(){
		currentEnnemy.SetHP(currentAlly.GetAtk() - currentEnnemy.GetDefense());
        if(currentEnnemy.GetHP() <= 0)
        {
            qte.GetComponent<qteScript>().showDead(currentEnnemy.team.name);
            currentEnnemy.Die();
            finishedCombat = true;
            yield return new WaitForSeconds(1);
        }
    }

	private IEnumerator engageCombat(CharacterScript c1,CharacterScript c2){

        qteScript qteScript = qte.GetComponent<qteScript>();
        // maybe, change minTime and maxTime according to currentAlly and currentEnnemy
        qteScript.minTime = 1f;
		qteScript.maxTime = 2f;
		qteScript.numberAnim = 4;

		int doubleAtk = 0;
		int val = c1.GetSpeed () - c2.GetSpeed ();
		if (val <= -5) {
			doubleAtk = -1;
		} else if (val >= 5) {
			doubleAtk = 1;
		}

		switch (doubleAtk) {
		case -1:
			currentAlly = c1;
			currentEnnemy = c2;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return StartCoroutine (Combat (c1, c2));
            if (finishedCombat) break;
            currentAlly = c2;
			currentEnnemy = c1;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return StartCoroutine (Combat (c2, c1));
            if (finishedCombat) break;
			currentAlly = c2;
			currentEnnemy = c1;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return StartCoroutine (Combat (c2, c1));
			break;
		case 0:
			currentAlly = c1;
			currentEnnemy = c2;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return  StartCoroutine (Combat (c1, c2));
            if (finishedCombat) break;
			currentAlly = c2;
			currentEnnemy = c1;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return  StartCoroutine (Combat (c2, c1));
			break;
		case 1:
			currentAlly = c1;
			currentEnnemy = c2;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return  StartCoroutine (Combat (c1, c2));
            if (finishedCombat) break;
            currentAlly = c2;
			currentEnnemy = c1;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return  StartCoroutine (Combat (c2, c1));
            if (finishedCombat) break;
			currentAlly = c1;
			currentEnnemy = c2;
            qteScript.numberQTE = calculateNbQTE(currentAlly.GetSpeed(), currentEnnemy.GetSpeed());
			yield return  StartCoroutine (Combat (c1, c2));
			break;
		}
        qte.GetComponent<qteScript>().showEnd();
        finishedCombat = false;
        yield return new WaitForSeconds(1);
    }

    private int calculateNbQTE(int v1, int v2)
    {
        if (v1 < v2)
        {
            return 4;
        }else if(v1 > v2)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    IEnumerator Combat(CharacterScript c1, CharacterScript c2)
    {
        qte.GetComponent<qteScript>().showTurn(c1.team.name);
        yield return new WaitForSeconds(2);
        //lancer QTE
        yield return StartCoroutine(qte.GetComponent<qteScript>().Animate());

    }

}