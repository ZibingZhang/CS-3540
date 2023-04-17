using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentProgress", 0);
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        int progress = PlayerPrefs.GetInt("CurrentProgress");
        SceneManager.LoadScene(progress*2 + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
