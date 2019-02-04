using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyier : IMovementType
{
    public int bonusHp = -2;
    public int bonusAtk = -2;
    public int bonusSpd = 5;
    public int bonusDef = -2;
    public int bonusRes = 5;
    public int movementNumber = 7;

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
