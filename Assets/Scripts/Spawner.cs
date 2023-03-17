using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public float spawnTime = 3;

    float xMin = -25;
    float xMax = 25;
    float y = 0;
    float zMin = -15;
    float zMax = 15;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 position;

        position.x = Random.Range(xMin, xMax);
        position.y = y;
        position.z = Random.Range(zMin, zMax);

        GameObject spawnedObj = Instantiate(obj, position, transform.rotation) as GameObject;
        spawnedObj.transform.parent = gameObject.transform;
    }
}
