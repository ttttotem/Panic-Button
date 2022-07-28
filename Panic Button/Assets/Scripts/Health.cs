using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public GameObject ragdoll;
    AudioManager am;

    private void Start()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

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
        if(ragdoll != null)
        {
            am.Play("Grunt");
            GameObject corpse = Instantiate(ragdoll, transform.position, transform.rotation);
            Destroy(corpse, 40f);
        }
        if(gameObject.tag == "Player")
        {
            GameManager gm = GameObject.Find("_GM").GetComponent<GameManager>();
            if (gm)
            {
                gm.PlayerDied();
            }
        }
        Destroy(gameObject);
    }

}
