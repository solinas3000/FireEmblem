using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : IWeaponType
{
    int bonusAtk = 2;
    int bonusSpd = -2;
    int range = 2;

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
