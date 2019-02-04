using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneLoader : ScriptableObject
{
    public GlobalString mainMenuName;
    public GlobalString level1Name;
    public GlobalString level2Name;
    public GlobalString level3Name;

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuName.Value);
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1Name.Value);
    }

    public void Level2()
    {
        SceneManager.LoadScene(level2Name.Value);
    }

    public void Level3()
    {
        SceneManager.LoadScene(level3Name.Value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
