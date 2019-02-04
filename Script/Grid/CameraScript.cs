using System;
using UnityEngine;

public class CameraScript : AbstractSingleton<CameraScript>
{
    public float speedRotation = 50f;
    public int speedZoom = 25;
    public float maxDeZoom;
    public float zoomFocus;
    private Vector3 oldPosition;
    private Vector3 oldRotation;
    private float oldZoom;
    private bool inCompleteView = false;
    public float axeTeam1;
    public float axeTeam2;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float oldPosZ = transform.GetChild(0).localPosition.z;
            transform.GetChild(0).localPosition = new Vector3(0, 0, Mathf.Clamp(oldPosZ + (speedZoom * Input.GetAxis("Mouse ScrollWheel")), -maxDeZoom, -20));
            setOld();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 oldRot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(Mathf.Clamp(oldRot.x - (speedRotation * Time.deltaTime),15,80), oldRot.y, oldRot.z);
            setOld();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 oldRot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(Mathf.Clamp(oldRot.x + (speedRotation * Time.deltaTime),15,80), oldRot.y, oldRot.z);
            setOld();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += new Vector3(0, -(speedRotation * Time.deltaTime), 0);
            setOld();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += new Vector3(0, speedRotation * Time.deltaTime, 0);
            setOld();
        }
    }

    public void setCameraTarget(Vector3 target, int teamNumber)
    {
        float axe;
        if(teamNumber ==1)
        {
            axe = axeTeam1-40;
        }
        else
        {
            axe = axeTeam2-40;
        }
        inCompleteView = false;
        transform.localPosition = new Vector3(target.x, 0, target.z);
        transform.eulerAngles = new Vector3(35, axe, 0);
        transform.GetChild(0).localPosition = new Vector3(0, 0, -zoomFocus);
        setOld();
    }

    public void switchCameraTargetToCompleteView(int teamNumberSelected)
    {
        if (inCompleteView)
        {
            inCompleteView = false;
            transform.GetChild(0).localPosition = new Vector3(0, 0, oldZoom);
            transform.eulerAngles = oldRotation;
            transform.localPosition = oldPosition;
        }
        else
        {
            setOld();
            setCameraTargetToCompleteView(teamNumberSelected);
        }
        
    }

    public void setCameraTargetToCompleteView(int teamNumberSelected)
    {
        float axe;
        if (teamNumberSelected == 1)
        {
            axe = axeTeam1;
        }
        else
        {
            axe = axeTeam2;
        }
        inCompleteView = true;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(60, axe, 0);
        transform.GetChild(0).localPosition = new Vector3(0, 0, -maxDeZoom);
    }

    private void setOld()
    {
        if (!inCompleteView)
        {
            oldZoom = transform.GetChild(0).localPosition.z;
            oldPosition = transform.localPosition;
            oldRotation = transform.eulerAngles;
        }
        
    }

    public void saveOld(Vector3 positionToSave, int teamNumber)
    {
        Debug.Log("in position to save");
        float axe;
        if (teamNumber == 1)
        {
            axe = axeTeam1-40;
        }
        else
        {
            axe = axeTeam2-40;
        }
        oldPosition = new Vector3(positionToSave.x, 0, positionToSave.z);
        oldRotation = new Vector3(35, axe, 0);
        oldZoom = -zoomFocus;
    }

}
