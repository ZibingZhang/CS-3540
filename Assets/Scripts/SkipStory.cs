using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipStory : MonoBehaviour
{
    public int sceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipScene();
        }
    }

    public void SkipScene()
    {
        switch (sceneIndex)
        {
            case 1:
                SceneManager.LoadScene("Level1 (Forest)");
                break;
            case 2:
                SceneManager.LoadScene("Level2 (Volcano)");
                break;
            case 3:
                SceneManager.LoadScene("Level3 (Ice)");
                break;
            case 4:
                SceneManager.LoadScene("Level4 (Sky)");
                break;
            case 5:
                SceneManager.LoadScene("MainMenu");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
