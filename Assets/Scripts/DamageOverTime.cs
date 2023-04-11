using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public int damage = 1;
    public int damageRate = 1;

    public AudioClip damageSFX;
    public GameObject damageImage = null;

    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        damageImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            damageImage.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && elapsedTime >= damageRate) 
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            AudioSource.PlayClipAtPoint(damageSFX, transform.position, 1);
            elapsedTime = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            damageImage.SetActive(false);
        }
    }
}
