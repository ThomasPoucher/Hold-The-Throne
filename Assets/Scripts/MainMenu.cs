using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneChanger.LoadScene("MainScene");
    }
    public void StartBonusGame()
    {
        SceneChanger.LoadScene("Level2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
