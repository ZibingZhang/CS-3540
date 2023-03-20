using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int startingHealth = 100;
    public Slider healthSlider;
    public Text healthText;
    public AudioClip hurtSFX;
    public AudioClip dieSFX;

    private int currentHealth;
    private LevelManager levelManager;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth + " / " + startingHealth;

        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage(int damageAmount)
    {
        //Debug.Log(name + " taken damage " + damageAmount);
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
            if (gameObject.tag == "Player")
            {
                var controller = gameObject.GetComponent<PlayerController>();
                controller.PlayAudioClip(hurtSFX);
            }
        }
    }

    void Dies()
    {
        if (gameObject.tag == "Player")
        {
            var controller = gameObject.GetComponent<PlayerController>();
            controller.PlayAudioClip(dieSFX);
            levelManager.LevelLost();
        }
        else
        {
            Debug.Log(name + " dies");
            Destroy(gameObject);

            if (gameObject.tag == "Enemy")
            {
                levelManager.EnemyDied();
            }
        }
    }

    public void GainHealth(int amount)
    {
        if (currentHealth < 100)
        {
            currentHealth += amount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
            healthText.text = currentHealth + " / " + startingHealth;
        }
    }

}