using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlatformSpawner : MonoBehaviour
{
    public GameObject obj;
    public float spawnTime = 10;
    public Transform[] bounds;
    
    private int platformIdx;
    private Transform stageBounds;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 position;

        platformIdx = Random.Range(0, bounds.Length);
        Transform currentPlatform = bounds[platformIdx];

        float yDirection = currentPlatform.localScale.y / 2 - 2;
        float xDirection = currentPlatform.localScale.x / 2 - 2;
        float zDirection = currentPlatform.localScale.z / 2 - 2;

        print(stageBounds.position.x);
        print(stageBounds.position.x + stageBounds.localScale.x / 2);

        Vector3 randomPosition = new Vector3(Mathf.Clamp(Random.Range(-xDirection, xDirection), stageBounds.position.x, stageBounds.position.x + stageBounds.localScale.x / 2),
                                          Random.Range(-yDirection, yDirection) + 1,
                                          Random.Range(-zDirection, zDirection));
        position = currentPlatform.position + randomPosition;

        GameObject spawnedObj = Instantiate(obj, position, Quaternion.Euler(0, 0, 90)) as GameObject;
    }
}
