using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 20;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Projectile"))
        {
            var health = gameObject.GetComponent<Health>();
            health.TakeDamage(damage);
            //Destroy(gameObject);
        }
    }
}
