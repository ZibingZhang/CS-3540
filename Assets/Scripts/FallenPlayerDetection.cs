using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenPlayerDetection : MonoBehaviour
{
    public int damageAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            var controller = other.gameObject.GetComponent<PlayerController>();
            controller.resetLocation = true;
            
            var health = other.gameObject.GetComponent<Health>();
            health.TakeDamage(damageAmount);
        }

    }
}
