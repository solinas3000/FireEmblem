using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenableVariable<T> : GlobalVariable<T> {

    public delegate void ChangeDelegate();
    public ChangeDelegate OnChange;

    public override T Value
    {
        get
        {
            return base.Value;
        }

        set
        {
            bool hasChanged = !value.Equals(this.Value);
            base.Value = value;
            if(hasChanged)
                NotifyListeners();
        }
    }

    private void NotifyListeners()
    {
        if (OnChange != null)
            OnChange.Invoke();
    }
}
