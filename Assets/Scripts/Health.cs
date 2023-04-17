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

    public int currentHealth;
    private LevelManager levelManager;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth*100/startingHealth;
        healthText.text = currentHealth + " / " + startingHealth;

        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage(int damageAmount)
    {
        if(gameObject.tag == "Enemy")
        {
            NinjaAI.currentState = NinjaAI.FSMStates.Damage;
        }
        //Debug.Log(name + " taken damage " + damageAmount);
        if (currentHealth > 0 && !LevelManager.levelPaused)
        {
            currentHealth -= damageAmount;
            healthSlider.value = Mathf.Clamp(currentHealth*100/startingHealth, 0, 100);
            //healthSlider.value = currentHealth;
            healthText.text = currentHealth + " / " + startingHealth;
        }
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                NinjaAI.currentState = NinjaAI.FSMStates.Dead;
                levelManager.EnemyDied();
            }
            else if (gameObject.tag == "Player")
            {
                var controller = gameObject.GetComponent<PlayerController>();

                if (!LevelManager.levelPaused)
                {
                    controller.PlayAudioClip(dieSFX);
                }
                levelManager.LevelLost();
            }
        } 
        else 
        {
            if (gameObject.tag == "Player" && !LevelManager.levelPaused)
            {
                var controller = gameObject.GetComponent<PlayerController>();
                controller.PlayAudioClip(hurtSFX);
            }
        }
    }
    public void GainHealth(int amount)
    {
        if (currentHealth < 100)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, 100);
            healthSlider.value = Mathf.Clamp(currentHealth*100/startingHealth, 0, 100);
            healthText.text = currentHealth + " / " + startingHealth;
        }
    }

}