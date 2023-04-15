using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapPickupIntroduction : MonoBehaviour
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
        text = "You have passed the second test.";
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
                    text = "You are now a fire bender.";
                    NextSentence();
                    break;
                case 1:
                    step++;
                    text = "You still have two masters to challenge.";
                    NextSentence();
                    break;
                case 2:
                    step++;
                    text = "The next test will be trickier.";
                    NextSentence();
                    break;
                case 3:
                    step++;
                    text = "There will be more traps.";
                    NextSentence();
                    break;
                case 4:
                    step++;
                    text = "Look out for these. Avoid them if possible.";
                    pickup.SetActive(true);
                    NextSentence();
                    break;
                case 5:
                    step++;
                    text = "They will take your health.";
                    NextSentence();
                    break;
                case 6:
                    step++;
                    text = "The next master is ready for you.";
                    NextSentence();
                    break;
                case 7:
                    step++;
                    text = "Be careful.";
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
