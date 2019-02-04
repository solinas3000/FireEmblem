using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementType
{
    int GetBonusAtk();
    int GetBonusSpd();
    int GetBonusDef();
    int GetBonusRes();
    int GetBonusHp();

    int GetMovementNumber();
}
