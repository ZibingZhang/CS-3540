using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPickup : MonoBehaviour
{
    public int subtractedHealth = 20;
    public int maxTime = 10;
    public Image timerWheel1;
    public Image timerWheel2;
    public GameObject player;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timerWheel1.fillAmount = currentTime / maxTime;
        timerWheel2.fillAmount = currentTime / maxTime;

        if (currentTime >= maxTime)
        {
            player.GetComponent<Health>().TakeDamage(subtractedHealth);
        }
        else if (currentTime % 0.5 == 0)
        {

        }
    }


}
