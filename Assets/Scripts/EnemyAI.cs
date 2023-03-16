using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float frequency = 5;

    private float timePassed = 0;
    private Transform player;
    private Transform projectileParent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > frequency)
        {
            FireAtPlayer();
            timePassed = 0;
        }
    }

    private void FireAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized + new Vector3(0, 0.05f, 0);
        GameObject projectile = Instantiate(projectilePrefab,
                transform.position + direction, transform.rotation);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * 50, ForceMode.VelocityChange);
        projectile.transform.SetParent(projectileParent);
        Debug.Log(Time.time + " fired at player");
    }
}
