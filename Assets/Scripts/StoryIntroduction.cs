using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryIntroduction : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public GameObject continueText;
    public float typingSpeed= 0.02f;
    public LevelManager levelManager;

    private string text = "";
    private bool typing = false;
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = "You are too foolish.";
        continueText.SetActive(false);
        NextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !typing)
        {
            continueText.SetActive(false);
            switch(step)
            {
                case 0:
                    step++;
                    text = "There are always threats to the peace between our four nations.";
                    NextSentence();
                    break;
                case 1:
                    step++;
                    text = "Being only a master of air is not enough as the Avatar. You must be a master of all four elements.";
                    NextSentence();
                    break;
                case 2:
                    step++;
                    text = "Water. Earth. Fire. Air.";
                    NextSentence();
                    break;
                case 3:
                    step++;
                    text = "The people do not recognize you as the Avatar.";
                    NextSentence();
                    break;
                case 4:
                    step++;
                    text = "You must gain more experience. You will compete in the Pro-Bending Arena against a master of each element.";
                    NextSentence();
                    break;
                case 5:
                    step++;
                    text = "After you have defeated one master, then you will become a master of their element as well.";
                    NextSentence();
                    break;
                case 6:
                    step++;
                    text = "Only once you have defeated the final master, will you be recognized as the Avatar.";
                    NextSentence();
                    break;
                case 7:
                    step++;
                    text = "Go. I will be watching.";
                    NextSentence();
                    break;
                case 8:
                    storyText.text = "";
                    levelManager.NextLevel();
                    break;
            }
        }
    }

    IEnumerator TypeStory()
    {
        typing = true;
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typing = false;
        continueText.SetActive(true);
    }
    public void NextSentence()
    {
        storyText.text = "";
        StartCoroutine(TypeStory());
    }
}