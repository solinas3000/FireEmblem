using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : AbstractSingleton<TextUpdater>
{
    public Text teamValue;
    public Text unitValue;
    public TMPro.TextMeshProUGUI gameOver;

    public void UpdateTeamValue(int i)
    {
        if (i == 1) teamValue.text = "BLACK";
        else teamValue.text = "WHITE";
    }

    public void UpdateUnitValue(CharacterScript c)
    {
        if (c != null)
            unitValue.text = "HP " + c.GetHP() +
                " ATK " + c.GetAtk() +
                " DEF " + c.GetDefense() +
                " SPD " + c.GetSpeed() +
                " RES " + c.GetResistance();
        else unitValue.text = "";
    }

    public void UpdateGameOver(string text)
    {
        gameOver.text = text;
    }
}
