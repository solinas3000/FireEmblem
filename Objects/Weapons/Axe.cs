using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour, IWeaponType
{
    int bonusAtk = 3;
    int bonusSpd = -3;
    int range = 1;

    public int GetAdvantage(IWeaponType weapon)
    {
        return 0;
    }

    public int GetAtk()
    {
        return bonusAtk;
    }

    public bool GetPhysicalType()
    {
        return true;
    }

    public int GetRange()
    {
        return range;
    }

    public int GetSpd()
    {
        return bonusSpd;
    }
}
