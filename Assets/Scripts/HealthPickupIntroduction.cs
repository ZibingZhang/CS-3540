using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPickupIntroduction : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public GameObject continueText;
    public GameObject pickup;
    public float typingSpeed= 0.02f;
    public LevelManager levelManager;

    private string text = "";
    private bool typing = false;
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = "You have passed the first test.";
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
                    text = "You are now an earth bender.";
                    NextSentence();
                    break;
                case 1:
                    step++;
                    text = "You still have three masters to challenge.";
                    NextSentence();
                    break;
                case 2:
                    step++;
                    text = "From now on your tests will be more difficult.";
                    NextSentence();
                    break;
                case 3:
                    step++;
                    text = "You will be fighting not only the master but also the environment.";
                    NextSentence();
                    break;
                case 4:
                    step++;
                    text = "If you find that you are losing health, look for one of these.";
                    pickup.SetActive(true);
                    NextSentence();
                    break;
                case 5:
                    step++;
                    text = "It will restore some of your health.";
                    NextSentence();
                    break;
                case 6:
                    step++;
                    text = "Your next trial awaits.";
                    NextSentence();
                    break;
                case 7:
                    step++;
                    text = "Good luck.";
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
