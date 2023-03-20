using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectileTrigger : MonoBehaviour
{
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
        GameObject otherObject = other.gameObject;
        if (otherObject.tag.Contains("Projectile"))
        {
            Debug.Log(name + " collided with projectile");
            Destroy(otherObject);
        }
    }
}
