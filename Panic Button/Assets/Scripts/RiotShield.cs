using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotShield : MonoBehaviour
{
    public float health = 100f;
    public GameObject ragdoll;

    public void TakeDamage(float Damage)
    {
        health = health - Damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject corpse = Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(corpse, 40f);
        Destroy(gameObject);

    }
}
