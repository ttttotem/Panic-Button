using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public GameObject muzzleFlash;
    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }
    void ShootBullet()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        flash.transform.parent = GameObject.Find("Player").transform;
        Destroy(flash, 0.2f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 20f);
    }
}
