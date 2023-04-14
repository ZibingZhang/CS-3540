using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool levelOver;

    [SerializeField] private Text announcementDisplay;
    //[SerializeField] private AudioClip winSFX;
    //[SerializeField] private AudioClip loseSFX;
    public static bool earth = false;
    public static bool fire = false;
    public static bool water = false;
    public static bool air = false;

    private int enemyCount;

    void Start()
    {
        levelOver = false;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Enemy count for level " + name + ": " + enemyCount);
    }

    public void EnemyDied()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            LevelWon();
        }
    }

    public void LevelWon()
    {
        levelOver = true;
        UpdateAnnouncement("you won :)");
        //AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        Invoke("NextLevel", 2);
    }

    public void LevelLost()
    {
        levelOver = true;
        UpdateAnnouncement("you lost :(");
        //AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        Invoke("ResetLevel", 2);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static float EnemyHeight()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        switch (currentSceneName)
        {
            case "Level1 (Forest)":
                return -1.5f;
            case "Level2 (Volcano)":
                return 0.5f;
            case "Level3 (Ice)":
                return 0.5f;
            case "Level4 (Sky)":
                return 0.5f;
            default:
                return 0;
        }
    }

    private void UpdateAnnouncement(string message)
    {
        announcementDisplay.text = message;
    }

    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Next level, current scene name: " + currentSceneName);
        switch (currentSceneName)
        {
            case "Level0 (Tutorial)":
                SceneManager.LoadScene("Level1Cutscene");
                break;
            case "Level1Cutscene":
                earth = true;
                SceneManager.LoadScene("Level1 (Forest)");
                break;
            case "Level1 (Forest)":
                earth = true;
                SceneManager.LoadScene("Level2 (Volcano)");
                break;
            case "Level2 (Volcano)":
                fire = true;
                SceneManager.LoadScene("Level3 (Ice)");
                break;
            case "Level3 (Ice)":
                water = true;
                SceneManager.LoadScene("Level4 (Sky)");
                break;
            case "Level4 (Sky)":
                // do nothing
                air = true;
                break;
        }
    }
}
