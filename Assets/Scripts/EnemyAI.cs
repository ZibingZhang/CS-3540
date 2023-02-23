using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timePassed = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 5)
        {
            FireAtPlayer();
            timePassed = 0;
        }
    }

    private void FireAtPlayer()
    {

        Debug.Log(Time.time + " fired at player");
    }
}
