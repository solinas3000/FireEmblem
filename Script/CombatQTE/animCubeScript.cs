using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCubeScript : MonoBehaviour
{
    public KeyCode key;
    Animator m_Animator;
    private bool keyDownActivated = true;
    public int animNumber;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        Debug.Log("key" + key.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key) && keyDownActivated)
        {
            m_Animator.SetTrigger("win");
			this.transform.GetComponentInParent<qteScript>().IncrementWin();
            DesactivateKeyDown();
        }
    }

    public void FinishAnim()
    {
        Debug.Log("FinishAnim");
        this.transform.GetComponentInParent<qteScript>().AddCube(animNumber);
        Destroy(gameObject);
    }

    public void DesactivateKeyDown()
    {
        Debug.Log("desactivation");
        keyDownActivated = false;
    }
}
