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
    public AudioClip pickedUpSFX; 
    public AudioClip tickNoiseSFX; 
    public AudioClip denotateSFX; 

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("PlayTick", 0, 0.5f);
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickedUp, Camera.main.transform.position);
            Destroy(gameObject, 0.5f);

        }
        
    }

    private void PlayTick()
    {
        AudioSource.PlayClipAtPoint(tickNoise, gameObject.transform.position);
    }


}
