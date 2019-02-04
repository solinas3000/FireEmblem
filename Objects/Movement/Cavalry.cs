using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalry : IMovementType
{
    public int bonusHp = 3;
    public int bonusAtk = 3;
    public int bonusSpd = -3;
    public int bonusDef = 3;
    public int bonusRes = -3;
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
