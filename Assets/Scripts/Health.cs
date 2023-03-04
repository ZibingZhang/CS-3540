using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int startingHealth = 100;
    int currentHealth;
    public Slider healthSlider;
    public Text healthText;
    public AudioClip hurtSFX;
    public AudioClip dieSFX;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth + " / " + startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
            healthText.text = currentHealth + " / " + startingHealth;
        }
        if (currentHealth <= 0)
        {
            Dies();
        } 
        else 
        {
            if (gameObject.tag == "Player") {
                var controller = gameObject.GetComponent<PlayerController>();
                controller.PlayAudioClip(hurtSFX);
            }
        }
    }

    void Dies()
    {
        if (gameObject.tag == "Player") {
            var controller = gameObject.GetComponent<PlayerController>();
            controller.PlayAudioClip(dieSFX);
        }
        else { 
            Debug.Log(name + " dies");
            Destroy(gameObject);
        }
    }

}