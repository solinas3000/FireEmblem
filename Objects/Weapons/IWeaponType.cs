using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponType
{
    int GetAtk();
    int GetSpd();
    int GetRange();
    int GetAdvantage(IWeaponType weapon);
    bool GetPhysicalType();
}
