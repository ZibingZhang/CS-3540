using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int addedHealth = 10;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public AudioClip audioPickup; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(audioPickup, Camera.main.transform.position);
            gameObject.SetActive(false);
            var health = other.GetComponent<Health>();
            health.GainHealth(addedHealth);

            Destroy(gameObject, 0.5f);

        }
        
    }

}
