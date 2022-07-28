using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float shootInterval = 2f;
    public GameObject muzzleFlash;
    public AudioClip clip;

    public float glowStickInterval = 7f;
    public GameObject glowStick;

    private void Start()
    {
        InvokeRepeating("ThrowGlowStick", Random.Range(0, 5), glowStickInterval + Random.Range(0,5));
        InvokeRepeating("ShootBullet", Random.Range(0, 5), shootInterval + +Random.Range(0, 5));
    }

    void ThrowGlowStick()
    {
        GameObject gs = Instantiate(glowStick, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = gs.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(gs, 20f);
    }

    void ShootBullet()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        flash.transform.parent = transform;
        Destroy(flash, 0.2f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 20f);
    }
}
