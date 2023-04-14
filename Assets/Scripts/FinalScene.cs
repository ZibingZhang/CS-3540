using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public RawImage fadeImg;
    public Color originalColor;
    public Color fadeOutColor;
    public GameObject continueText;
    public float typingSpeed= 0.03f;
    public LevelManager levelManager;

    private string text = "";
    private bool typing = false;
    private int step = 0;
    private float timePassed = 0;
    private bool fadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        text = "You have beaten me.";
        continueText.SetActive(false);
        NextSentence();
        fadeOut = false;
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
                    text = "You have now mastered all four elements.";
                    NextSentence();
                    break;
                case 1:
                    step++;
                    text = "You are now worthy of being called the Avatar.";
                    NextSentence();
                    break;
                case 2:
                    step++;
                    text = "You now bear a great responsibility.";
                    NextSentence();
                    break;
                case 3:
                    step++;
                    text = "All of the people depend on you.";
                    NextSentence();
                    break;
                case 4:
                    step++;
                    text = "Thank you, Avatar.";
                    NextSentence();
                    break;
                case 5:
                    storyText.text = "";
                    fadeOut = true;
                    Invoke("CallMenu", 3);
                    break;
            }
        }

        if (fadeOut)
        {
            timePassed += Time.deltaTime;
            fadeImg.color = Color.Lerp(originalColor, fadeOutColor, timePassed / 3);
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

    private void CallMenu()
    {
        levelManager.NextLevel();
    }
}
