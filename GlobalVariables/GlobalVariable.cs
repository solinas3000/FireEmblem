using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable<T> : ScriptableObject {

    [SerializeField]
    private T value;

    public virtual T Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }
}
