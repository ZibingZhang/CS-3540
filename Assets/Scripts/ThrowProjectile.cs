using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    public AudioClip playerSFX;
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    private Transform projectileParent;

    // Start is called before the first frame update
    void Start()
    {
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(playerSFX, transform.position, 1);
            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation) ;
            Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            projectile.transform.SetParent(projectileParent);
        }
    }
}
