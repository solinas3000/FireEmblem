using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCase : MonoBehaviour
{
    private void OnMouseDown()
    {
        this.gameObject.transform.parent.GetChild(0).GetComponent<CaseScript>().OnMouseDown();
    }
}
