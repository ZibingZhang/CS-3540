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

    void Start()
    {
        elementList = new GameObject[4] { earth, fire, water, air };
        progress = PlayerPrefs.GetInt("CurrentProgress");
        for (int i = 0; i < 4; i++)
        {
            if(i < progress)
            {
                GameObject temp = elementList[i];
                temp.SetActive(true);
            }
        }
    }

    public void EarthLevel()
    {
        SceneManager.LoadScene("Level1 (Forest)");
    }

    public void FireLevel()
    {
        SceneManager.LoadScene("Level2 (Volcano)");
    }

    public void WaterLevel()
    {
        SceneManager.LoadScene("Level3 (Ice)");
    }

    public void AirLevel()
    {
        SceneManager.LoadScene("Level4 (Sky)");
    }

}
