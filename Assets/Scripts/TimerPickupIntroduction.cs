using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerPickupIntroduction : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public GameObject continueText;
    public GameObject pickup;
    public float typingSpeed= 0.03f;
    public LevelManager levelManager;

    private string text = "";
    private bool typing = false;
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = "You have passed the third test.";
        continueText.SetActive(false);
        pickup.SetActive(false);
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
                    text = "You are now a water bender.";
                    NextSentence();
                    break;
                case 1:
                    step++;
                    text = "There is only one test left.";
                    NextSentence();
                    break;
                case 2:
                    step++;
                    text = "The last test is the trickiest.";
                    NextSentence();
                    break;
                case 3:
                    step++;
                    text = "If you see one of these, you should reach it before time runs out.";
                    pickup.SetActive(true);
                    NextSentence();
                    break;
                case 4:
                    step++;
                    text = "If not, it will take your health.";
                    NextSentence();
                    break;
                case 5:
                    step++;
                    text = "You can find them more easily using their sound.";
                    NextSentence();
                    break;
                case 6:
                    step++;
                    text = "I will be your challenger this time.";
                    NextSentence();
                    break;
                case 7:
                    step++;
                    text = "Be ready.";
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
