using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public float spawnTime = 10;

    private Transform stageBounds;
    private Vector3 playerBounds;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
        playerBounds = new Vector3(stageBounds.localScale.x / 2 - 5, 0, stageBounds.localScale.z - 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 position;

        Vector3 origin = stageBounds.position;
        origin.x = origin.x + stageBounds.localScale.x / 4;
        Vector3 range = playerBounds / 2.0f;
        Vector3 randomPosition = new Vector3(Random.Range(-range.x, range.x),
                                          2,
                                          Random.Range(-range.z, range.z));
        position = origin + randomPosition;

        GameObject spawnedObj = Instantiate(obj, position, transform.rotation) as GameObject;
        spawnedObj.transform.parent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
    }
}
