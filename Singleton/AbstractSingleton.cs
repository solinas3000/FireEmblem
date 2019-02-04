using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSingleton<T> : MonoBehaviour where T:AbstractSingleton<T>{
    private static T instance;
    public bool persistent;

    private void Awake()
    {
        if (!instance)
        {
            foreach(var item in FindObjectsOfType<T>())
            {
                if (item.persistent)
                {
                    instance = item;
                    break;
                }
            }
            if (!instance)
                instance = (T)this;
        }
        if (instance && instance != this)
            Destroy(gameObject);
    }

    public static T GetInstance()
    {
        if (!instance)
            instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
        return instance;
    }
}
