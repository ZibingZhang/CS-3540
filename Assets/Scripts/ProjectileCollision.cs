using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 20;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Projectile"))
        {
            Debug.Log(name + " collided with projectile");
            var health = gameObject.GetComponent<Health>();
            health.TakeDamage(damage);
            Destroy(other.gameObject);
        }
    }
}
