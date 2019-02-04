using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : IMovementType
{
    public int bonusHp = 5;
    public int bonusAtk = 5;
    public int bonusSpd = -5;
    public int bonusDef = 5;
    public int bonusRes = -5;
    public int movementNumber = 4;
    

    public int GetBonusAtk()
    {
        return bonusAtk;
    }

    public int GetBonusDef()
    {
        return bonusDef;
    }

    public int GetBonusHp()
    {
        return bonusHp;
    }

    public int GetBonusRes()
    {
        return bonusRes;
    }

    public int GetBonusSpd()
    {
        return bonusSpd;
    }

    public int GetMovementNumber()
    {
        return movementNumber;
    }
}
