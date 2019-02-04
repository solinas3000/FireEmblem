using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCharacter : ScriptableObject
{
    public object SetupMouvement(EnumMouvement.MouvementEnum mvt)
    {

        switch (mvt)
        {
            case EnumMouvement.MouvementEnum.Armored:
                return new Armored();
            case EnumMouvement.MouvementEnum.Cavalry:
                return new Cavalry();
            case EnumMouvement.MouvementEnum.Flyier:
                return new Flyier();
            case EnumMouvement.MouvementEnum.Infantry:
                return new Infantry();
            default:
                return null;
        }
    }

    public object SetupWeapon(EnumWeapon.WeaponEnum weapon)
    {
        switch (weapon)
        {
            case EnumWeapon.WeaponEnum.Axe:
                return new Axe();
            case EnumWeapon.WeaponEnum.Bow:
                return new Bow();
            case EnumWeapon.WeaponEnum.Lance:
                return new Lance();
            case EnumWeapon.WeaponEnum.Magic:
                return new Magic();
            case EnumWeapon.WeaponEnum.Sword:
                return new Sword();
            default:
                return null;
        }
    }
}
