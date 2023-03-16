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

    private void UpdateAnnouncement(string message)
    {
        announcementDisplay.text = message;
    }

    private void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Next level, current scene name: " + currentSceneName);
        switch (currentSceneName)
        {
            case "Level1 (Forest)":
                SceneManager.LoadScene("Level2 (Volcano)");
                break;
            case "Level2 (Volcano)":
                SceneManager.LoadScene("Level3 (Ice)");
                break;
            case "Level3 (Ice)":
                SceneManager.LoadScene("Level4 (Sky)");
                break;
            case "Level4 (Sky)":
                // do nothing
                break;
        }
    }
}
