using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowProjectile : MonoBehaviour
{

    public enum Element
    {
        Air,
        Earth,
        Fire,
        Water
    }
    public static Element currentElement;

    public AudioClip waterSFX;
    public AudioClip earthSFX;
    public AudioClip fireSFX;
    public AudioClip airSFX;
    //public int currentElement = 0;

    public int attackRefreshRate = 1;
    public int specialRefreshRate = 5;
    public Image attackCharge;
    public Image specialCharge;

    [SerializeField] private GameObject airProjectilePrefab;
    [SerializeField] private GameObject airSpecialPrefab;
    [SerializeField] private GameObject earthProjectilePrefab;
    [SerializeField] private GameObject earthSpecialPrefab;
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private GameObject fireSpecialPrefab;
    [SerializeField] private GameObject waterProjectilePrefab;
    [SerializeField] private GameObject waterSpecialPrefab;
    [SerializeField] private float projectileSpeed = 100;
    [SerializeField] private float specialSpeed = 100;

    private Transform projectileParent;

    private GameObject currentProjectile;
    private GameObject currentSpecial;
    private float elapsedTimeAttack = 0f;
    private float elapsedTimeSpecial = 0f;
    private AudioClip currentAudio;

    // Start is called before the first frame update
    void Start()
    {
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

        elapsedTimeAttack = (float)attackRefreshRate;
        elapsedTimeSpecial = (float)specialRefreshRate;

        attackCharge.fillAmount = 0;
        specialCharge.fillAmount = 0;

        SetElementObjects(currentElement);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.levelPaused)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SwitchElement();
            }

            if (Input.GetButtonDown("Fire1") && elapsedTimeAttack >= attackRefreshRate)
            {
                FireProjectile(currentProjectile, projectileSpeed);
                elapsedTimeAttack = 0f;
            } 
            else if (Input.GetButtonDown("Fire2") && elapsedTimeSpecial >= specialRefreshRate)
            {
                FireProjectile(currentSpecial, specialSpeed);
                elapsedTimeSpecial = 0f;
            }

            elapsedTimeAttack += Time.deltaTime;
            elapsedTimeSpecial += Time.deltaTime;

            UpdateRefreshUI();
        }

    }
    void SetElementObjects(Element element)
    {
        currentElement = element;
        switch (element)
        {
            case Element.Water:
                currentAudio = waterSFX;
                currentProjectile = waterProjectilePrefab;
                currentSpecial = waterSpecialPrefab;
                break;
            case Element.Air:
                currentAudio = airSFX;
                currentProjectile = airProjectilePrefab;
                currentSpecial = airSpecialPrefab;
                break;
            case Element.Fire:
                currentAudio = fireSFX;
                currentProjectile = fireProjectilePrefab;
                currentSpecial = fireSpecialPrefab;
                break;
            case Element.Earth:
                currentAudio = earthSFX;
                currentProjectile = earthProjectilePrefab;
                currentSpecial = earthSpecialPrefab;
                break;

        }
    }
    void FireProjectile(GameObject prefab, float speed)
    {
        AudioSource.PlayClipAtPoint(currentAudio, transform.position, 1);
        GameObject projectile = Instantiate(prefab,
            transform.position + transform.forward, transform.rotation);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        projectile.transform.parent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

    }

    private void SwitchElement()
    {
        print(currentElement);
        if (currentElement == Element.Water)
        {
            SetElementObjects(Element.Air);
        }
        else if (currentElement == Element.Earth)
        {
            SetElementObjects(Element.Fire);
        }
        else if (currentElement == Element.Air)
        {
            SetElementObjects(Element.Earth);
        }
        else if (currentElement == Element.Fire)
        {
            SetElementObjects(Element.Water);
        }
    }

    private void UpdateRefreshUI() 
    {
        if (elapsedTimeAttack >= attackRefreshRate)
        {
            attackCharge.fillAmount = 100;
        } 
        else 
        {
            attackCharge.fillAmount = elapsedTimeAttack / attackRefreshRate;
        }

        if (elapsedTimeSpecial >= specialRefreshRate)
        {
            specialCharge.fillAmount = 100;
        } 
        else 
        {
            specialCharge.fillAmount = elapsedTimeSpecial / specialRefreshRate;
        }
    }
}
