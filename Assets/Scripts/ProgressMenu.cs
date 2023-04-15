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

    void start()
    {

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        earth.SetActive(LevelManager.earth);
        fire.SetActive(LevelManager.fire);
        water.SetActive(LevelManager.water);
        air.SetActive(LevelManager.air);
    }
}
