using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextUpdater : AbstractSingleton<CombatTextUpdater>
{
    public TMPro.TextMeshProUGUI value;

    public void UpdateValue(string text)
    {
        value.text = text;
    }
}