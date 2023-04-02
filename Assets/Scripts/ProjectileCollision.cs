using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 20;

    
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag.Contains("Projectile"))
        {
            //Debug.Log(name + " collided with projectile");
            damage = otherObject.GetComponent<ProjectileBehavior>().damageAmount;
            var health = gameObject.GetComponent<Health>();
            var effect = gameObject.GetComponent<ProjectileEffect>();
            print("Name:" + other.gameObject.name);
            effect.AssignEffect(other.gameObject.name, other);
            health.TakeDamage(damage);
            Destroy(other.gameObject);
        }
    }
}
