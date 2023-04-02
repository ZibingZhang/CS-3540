using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    float elapsedTime = 0;

    CharacterController playerController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void AssignEffect(string name, Collider projectile)
    {
        if (name.Contains("Special"))
        {
            if(name.Contains("Earth"))
            {

            }
            else if (name.Contains("Fire"))
            {

            }
            else if (name.Contains("Water"))
            {

            }
            //air
            else
            {

            }   
        }
        else
        {
            if (name.Contains("Earth"))
            {
                StartCoroutine(KnockBack(projectile, 100));
            }
            else if (name.Contains("Fire"))
            {
                StartCoroutine(DamageOverTime(5, 2));
            }
            else if (name.Contains("Water"))
            {

            }
            //air
            else
            {

            }
        }
    }

    IEnumerator KnockBack(Collider projectile, int amt)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Vector3 direction = projectile.transform.position.normalized;
        print(direction);
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

        /*
        if (elapsedTime > 2.0f)
        {
            print(elapsedTime);
            print("In enable");
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        */
        /*
        if (rb.velocity.magnitude < Vector3.one.magnitude)
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        */

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
