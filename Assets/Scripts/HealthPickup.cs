using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int value = 10;

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
            gameObject.SetActive(false);
            var health = other.GetComponent<Health>();
            health.GainHealth(value);

            Destroy(gameObject, 0.5f);

        }
        
    }

}
