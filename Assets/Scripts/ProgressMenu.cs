using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressMenu : MonoBehaviour
{
    public GameObject earth;
    public GameObject fire;
    public GameObject water;
    public GameObject air;

    GameObject[] elementList;
    int progress;
    int currentMaxProgress;
    int maxProgress;

    void Start()
    {
        elementList = new GameObject[4] { earth, fire, water, air };
        progress = PlayerPrefs.GetInt("CurrentProgress");
        currentMaxProgress = PlayerPrefs.GetInt("MaxProgress", 0);

        if (!(progress > 4))
        {
            PlayerPrefs.SetInt("MaxProgress", Mathf.Max(progress, currentMaxProgress));
        }

        maxProgress = PlayerPrefs.GetInt("MaxProgress");

        for (int i = 0; i < 4; i++)
        {
            if(i < maxProgress)
            {
                GameObject temp = elementList[i];
                temp.SetActive(true);
            }
        }
    }

    public void EarthLevel()
    {
        SceneManager.LoadScene("Level1 (Forest)");
        PlayerPrefs.SetInt("CurrentProgress", 1);
    }

    public void FireLevel()
    {
        SceneManager.LoadScene("Level2 (Volcano)");
        PlayerPrefs.SetInt("CurrentProgress", 2);
    }

    public void WaterLevel()
    {
        SceneManager.LoadScene("Level3 (Ice)");
        PlayerPrefs.SetInt("CurrentProgress", 3);
    }

    public void AirLevel()
    {
        SceneManager.LoadScene("Level4 (Sky)");
        PlayerPrefs.SetInt("CurrentProgress", 4);
    }

}
