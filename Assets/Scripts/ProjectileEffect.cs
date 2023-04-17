using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    float elapsedTime = 0;

    CharacterController playerController;

    public int earthKnockback = 100;
    public int earthKnockbackSpecial = 200;

    public int fireTime = 4;
    public int fireDamage = 2;
    public int fireTimeSpecial = 8;
    public int fireDamageSpecial = 2;

    public int waterTime = 3;
    public int waterTimeSpecial = 4;
    public GameObject frostImage = null;

    public int airKnockback = 5;
    public int airKnockbackSpecial = 10;

    void Start()
    {
        frostImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void AssignEffect(string name, Collider projectile)
    {
        Vector3 back = projectile.transform.position.normalized;

        if (name.Contains("Special"))
        {
            if(name.Contains("Earth"))
            {
                StartCoroutine(KnockBack(earthKnockbackSpecial, back));
            }
            else if (name.Contains("Fire"))
            {
                StartCoroutine(DamageOverTime(fireTimeSpecial, fireDamageSpecial));
            }
            else if (name.Contains("Water") || name.Contains("Snow"))
            {
                StartCoroutine(Freeze(waterTimeSpecial));
            }
            //air
            else
            {
                StartCoroutine(Spin());
            }   
        }
        else
        {
            if (name.Contains("Earth"))
            {
                StartCoroutine(KnockBack(earthKnockback, back));
            }
            else if (name.Contains("Fire"))
            {
                StartCoroutine(DamageOverTime(fireTime, fireDamage));
            }
            else if (name.Contains("Water") || name.Contains("Snow"))
            {
                StartCoroutine(Freeze(waterTime));
            }
            else
            {
                StartCoroutine(Spin());
            }
        }
    }

    IEnumerator Spin()
    {
        Transform t = gameObject.transform;
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            t.rotation = Quaternion.Euler(new Vector3(t.rotation.x, Random.Range(90, 270), t.rotation.z));
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        /*
        else
        {
            gameObject.GetComponent<NinjaAI>().enabled = false;
            t.rotation = Quaternion.Euler(new Vector3(t.rotation.x, t.rotation.y, t.rotation.z));
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<NinjaAI>().enabled = true;
        }
        */
    }

   

    IEnumerator Freeze(int duration)
    {
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            frostImage.SetActive(true);
            yield return new WaitForSeconds(duration);
            gameObject.GetComponent<CharacterController>().enabled = true;
            frostImage.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<NinjaAI>().enabled = false;
            yield return new WaitForSeconds(duration);
            gameObject.GetComponent<NinjaAI>().enabled = true;
        }
    }

    IEnumerator KnockBack(int amt, Vector3 direction)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        //Vector3 direction = projectile.transform.position.normalized;
        //direction = Vector3.back;
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;

            //elapsedTime = 0;

            rb.AddForce(direction * amt);

            yield return new WaitForSeconds(1);

            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<NinjaAI>().enabled = false;
            rb.AddForce(direction * 1000);

            yield return new WaitForSeconds(5);

            gameObject.GetComponent<NinjaAI>().enabled = true;
        }
    }

    IEnumerator DamageOverTime(int time, int damage)
    {
        var health = gameObject.GetComponent<Health>();
        for (int i = 0; i < time; i++)
        {
            health.TakeDamage(damage);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
