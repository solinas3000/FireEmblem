using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : IWeaponType
{
    int bonusAtk = -1;
    int bonusSpd = 1;
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
