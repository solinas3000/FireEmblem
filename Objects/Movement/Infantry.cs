using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : IMovementType
{
    public int bonusHp = 0;
    public int bonusAtk = 0;
    public int bonusSpd = 0;
    public int bonusDef = 0;
    public int bonusRes = 0;
    public int movementNumber = 6;

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
