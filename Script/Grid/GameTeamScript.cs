using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTeamScript : AbstractSingleton<GameTeamScript>
{
    public int currentTeamNumber=1;
    public CameraScript cameraScript;
    public TextUpdater textUpdater;
    public UnityEvent OnGameOver;

    public void Start()
    {
        cameraScript = CameraScript.GetInstance();
        textUpdater = TextUpdater.GetInstance();
        cameraScript.switchCameraTargetToCompleteView(currentTeamNumber);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cameraScript.switchCameraTargetToCompleteView(currentTeamNumber);
        }
    }

    public void ChangeTurn()
    {
        TeamScript tS1 = transform.GetChild(0).GetComponent<TeamScript>();
        TeamScript tS2 = transform.GetChild(1).GetComponent<TeamScript>();
        GameTeamScript.GetInstance().StartCoroutine(checkGameOver(tS1,tS2.name));
        GameTeamScript.GetInstance().StartCoroutine(checkGameOver(tS2,tS1.name));
        if (currentTeamNumber == 1)
        {
            currentTeamNumber = 2;
            cameraScript.saveOld(calculateBestPosition(tS2), currentTeamNumber);
            cameraScript.setCameraTargetToCompleteView(currentTeamNumber);
        }
        else
        {
            currentTeamNumber = 1;
            cameraScript.saveOld(calculateBestPosition(tS1), currentTeamNumber);
            cameraScript.setCameraTargetToCompleteView(currentTeamNumber);
        }
        textUpdater.UpdateTeamValue(GameTeamScript.GetInstance().currentTeamNumber);
        textUpdater.UpdateUnitValue(null);
    }

    private Vector3 calculateBestPosition(TeamScript tS)
    {
        int number = 0;
        Vector3 sum = new Vector3(0,0,0);
        foreach(Transform c in tS.characters)
        {
            sum += c.parent.localPosition;
            number++;
        }
        return sum / number;
    }

    private IEnumerator checkGameOver(TeamScript tS,String winner)
    {
        Debug.Log("in checkOver");
        if(tS.characters.Count == 0)
        {
            //Show tS.teamNumber winner
            TextUpdater.GetInstance().UpdateGameOver("End Of Party\n" + winner + "\nwin the party !");
            //End Game and Return To Menu 
            yield return new WaitForSeconds(2);
            OnGameOver.Invoke();
        }
    }
}
