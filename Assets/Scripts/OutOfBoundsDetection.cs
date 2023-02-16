using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsDetection : MonoBehaviour
{
    void Start()
    {
        // clean up remaining objects not deleted from out of bounds detection
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tags = collision.gameObject.tag;
        if (tags.Contains("Boundary") || tags.Contains("Stage"))
        {
            Destroy(gameObject);
        }
    }
}
