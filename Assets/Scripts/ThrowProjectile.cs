using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowProjectile : MonoBehaviour
{

    public enum Element
    {
        Water,
        Earth,
        Fire,
        Air
    }
    public static Element currentElement;

    public AudioClip waterSFX;
    public AudioClip earthSFX;
    public AudioClip fireSFX;
    public AudioClip airSFX;
    //public int currentElement = 0;

    public int attackRefreshRate = 1;
    public int specialRefreshRate = 5;
    public Text attackText;
    public Text specialText;
    public Image attackCharge;
    public Image specialCharge;
    
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

  /*      if (waterProjectilePrefab != null && waterSpecialPrefab != null) {
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
        }*/

        SetElementObjects(currentElement);

        //Debug.Log(projectiles.Count);
        //Debug.Log(specials.Count);
        
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
        //projectile.transform.SetParent(projectileParent);
        projectile.transform.parent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

    }

    private void SwitchElement()
    {
        if (currentElement == Element.Water)
        {
            SetElementObjects(Element.Earth);
        }
        else if (currentElement == Element.Earth)
        {
            SetElementObjects(Element.Fire);
        }
        else if (currentElement == Element.Fire)
        {
            SetElementObjects(Element.Air);
        }
        else if (currentElement == Element.Air)
        {
            SetElementObjects(Element.Water);
        }
    }

    private void UpdateRefreshUI() 
    {
        if (elapsedTimeAttack >= attackRefreshRate)
        {
            attackText.text = "<b>ATTACK CHARGED</b>";
            attackText.color = Color.black;
            attackCharge.fillAmount = 0;
        } 
        else 
        {
            attackText.text = "attack charging... ";
            attackText.color = Color.white;
            attackCharge.fillAmount = elapsedTimeAttack / attackRefreshRate;
        }

        if (elapsedTimeSpecial >= specialRefreshRate)
        {
            specialText.text = "<b>SPECIAL CHARGED</b>";
            specialText.color = Color.black;
            specialCharge.fillAmount = 0;
        } 
        else 
        {
            specialText.text = "special charging... ";
            specialText.color = Color.white;
            specialCharge.fillAmount = elapsedTimeSpecial / specialRefreshRate;
        }
    }
}
