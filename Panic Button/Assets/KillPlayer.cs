using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            source.PlayOneShot(clip);
            Health health = collision.GetComponent<Health>();
            if (health)
            {
                health.TakeDamage(10000000000f);
            }
        }
    }
}
