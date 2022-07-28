using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffectBleed;
    public GameObject hitEffectExplode;
    public GameObject bloodSplatter;
    public float damage = 20f;

    private void Start()
    {
        transform.Rotate(Vector3.forward * 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        RiotShield riotShield = collision.GetComponent<RiotShield>();
        if (health)
        {
            health.TakeDamage(damage);
            GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
            GameObject effect = Instantiate(hitEffectBleed, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
            Destroy(splatter, 50f);
            Destroy(gameObject);
        } else if (riotShield)
        {
            riotShield.TakeDamage(damage);
            GameObject effect = Instantiate(hitEffectExplode, transform.position, Quaternion.identity);
            effect.transform.parent = collision.transform;
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        } 
        else if(collision.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffectExplode, transform.position, Quaternion.identity);
            effect.transform.parent = collision.transform;
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject,20f);
        }
    }
}
