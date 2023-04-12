using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public int boost = 2;

    public AudioClip speedSFX;

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
        if (other.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<PlayerController>().ChangeSpeed(boost);
            AudioSource.PlayClipAtPoint(speedSFX, transform.position, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<PlayerController>().ChangeSpeed(1.0f / boost);
            AudioSource.PlayClipAtPoint(speedSFX, transform.position, 1);
        }
    }
}
