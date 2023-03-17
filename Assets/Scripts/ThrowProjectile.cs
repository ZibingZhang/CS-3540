using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    public AudioClip playerSFX;
    public int currentElement = 0;
    
    [SerializeField] private GameObject waterProjectilePrefab;
    [SerializeField] private GameObject waterSpecialPrefab;
    [SerializeField] private GameObject earthProjectilePrefab;
    [SerializeField] private GameObject earthSpecialPrefab;
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private GameObject fireSpecialPrefab;
    [SerializeField] private GameObject airProjectilePrefab;
    [SerializeField] private GameObject airSpecialPrefab;
    [SerializeField] private float projectileSpeed = 100;
    [SerializeField] private float specialSpeed = 100;

    private Transform projectileParent;
    private List<GameObject> projectiles = new List<GameObject>();
    private List<GameObject> specials = new List<GameObject>();
    private GameObject currentProjectile;
    private GameObject currentSpecial;

    // Start is called before the first frame update
    void Start()
    {
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

        if (waterProjectilePrefab != null && waterSpecialPrefab != null) {
            projectiles.Add(waterProjectilePrefab);
            specials.Add(waterSpecialPrefab);
        }
        if (earthProjectilePrefab != null && earthSpecialPrefab != null) {
            projectiles.Add(earthProjectilePrefab);
            specials.Add(earthSpecialPrefab);
        }
        if (fireProjectilePrefab != null && fireSpecialPrefab != null) {
            projectiles.Add(fireProjectilePrefab);
            specials.Add(fireSpecialPrefab);
        }
        if (airProjectilePrefab != null && airSpecialPrefab != null) {
            projectiles.Add(airProjectilePrefab);
            specials.Add(airSpecialPrefab);
        }

        currentProjectile = projectiles[currentElement];
        currentSpecial = specials[currentElement];

        Debug.Log(projectiles.Count);
        Debug.Log(specials.Count);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchElement();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(playerSFX, transform.position, 1);
            GameObject projectile = Instantiate(currentProjectile,
                transform.position + transform.forward, transform.rotation) ;
            Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            projectile.transform.SetParent(projectileParent);
        } else if (Input.GetButtonDown("Fire2"))
        {
            AudioSource.PlayClipAtPoint(playerSFX, transform.position, 1);
            GameObject projectile = Instantiate(currentSpecial,
                transform.position + transform.forward, transform.rotation) ;
            Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * specialSpeed, ForceMode.VelocityChange);
            projectile.transform.SetParent(projectileParent);
        }
    }

    private void SwitchElement() {
        currentElement = (currentElement + 1) % projectiles.Count;

        currentProjectile = projectiles[currentElement];
        currentSpecial = specials[currentElement];
    }
}
