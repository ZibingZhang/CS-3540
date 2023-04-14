using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructorScript : MonoBehaviour
{
    public Text instructions;
    public Transform textPanel;
    public float typingSpeed= 0.02f;
    private GameObject player;
    private Health enemyHealth;
    public Transform eyes;
    private float talkDistance = 5;
    private float fieldOfView = 45;
    private float scalar = 400;
    private string text = "";
    private RectTransform panelRectTransform;
    private LevelManager levelManager;

    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetInteger("animState", 0);
        eyes = GameObject.FindGameObjectWithTag("Eyes").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();

        text = "Welcome to the tutorial. Begin by speaking with me. I'm over here! To your left. Use the arrow keys to move around and the mouse to look around.";

        levelManager = FindObjectOfType<LevelManager>();
        panelRectTransform = textPanel.GetComponent<RectTransform>();

        NextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        switch(step)
        {
            case 0:
                if (IsPlayerInFOV())
                {
                    step++;
                    text = "You can also use the spacebar to jump!";
                    NextSentence();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    step++;
                    text = "Your task is to defeat the opponent in each level. Press left click to attack. Your attacks have a cooldown. You can see cooldowns on the bottom left.";

                    NextSentence();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    step++;
                    text = "Press right click to special attack. These attacks have a longer cooldown.";

                    NextSentence();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    step++;
                    text = "You can use multiple different elements. Press 'z' to switch elements. You can see which element you are using in the bottom panel.";

                    NextSentence();                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    step++;
                    text = "Each element has a different effect. Now attack with the new element.";

                    NextSentence();
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    step++;
                    text = "That's everything! Defeat the enemy across the arena from you to continue into the actual game. The enemy's health bar is on the top right, in red. The other health bar is your own. Don't let your own health reach zero!";

                    NextSentence();
                }
                break;
            case 6:
                if (enemyHealth.currentHealth <= 0)
                {
                    levelManager.LevelWon();
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 frontRayPoint = eyes.position + (eyes.forward * talkDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(eyes.position, frontRayPoint, Color.red);
        Debug.DrawLine(eyes.position, leftRayPoint, Color.red);
        Debug.DrawLine(eyes.position, rightRayPoint, Color.red);
    }

    bool IsPlayerInFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - eyes.position;
        if (Vector3.Angle(directionToPlayer, eyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(eyes.position, directionToPlayer, out hit, talkDistance))
            {
                // can someone try to help figure out why this isn't fucking working
                return true;
            }
        }
        return false;
    }
    IEnumerator TypeInstructions()
    {
        float speed = player.GetComponent<PlayerController>().moveSpeed;
        player.GetComponent<PlayerController>().moveSpeed = 0;
        foreach (char letter in text.ToCharArray())
        {
            instructions.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        player.GetComponent<PlayerController>().moveSpeed = speed;
    }
    public void NextSentence()
    {
        float numLines = Mathf.Ceil(text.ToCharArray().Length/40) * scalar;
        print(numLines);
        numLines = Mathf.Clamp(numLines, 750, 2000);
        print("after clamp" + numLines);
        if (numLines == 0)
        {
            panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, 0f);
        }
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x,
            numLines + 100);
        instructions.text = "";
        StartCoroutine(TypeInstructions());

    }
}
