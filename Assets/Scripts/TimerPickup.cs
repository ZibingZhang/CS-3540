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
    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("PlayTick", 0, 0.5f);
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timerWheel1.fillAmount = currentTime / maxTime;
        timerWheel2.fillAmount = currentTime / maxTime;

        if (currentTime >= maxTime && active)
        {
            player.GetComponent<Health>().TakeDamage(subtractedHealth);
            AudioSource.PlayClipAtPoint(denotateSFX, gameObject.transform.position);
            gameObject.SetActive(false);
            active = false;
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            AudioSource.PlayClipAtPoint(pickedUpSFX, Camera.main.transform.position);
            active = false;
            Destroy(gameObject, 0.5f);
        }
        
    }

    private void PlayTick()
    {
        AudioSource.PlayClipAtPoint(tickNoiseSFX, gameObject.transform.position);
    }


}
