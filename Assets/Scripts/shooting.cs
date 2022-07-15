using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firepos;
    public Transform Gun;
    public GameObject bulletPrefab;
    public AudioSource laserSound;
    public AudioSource explosionSound1, explosionSound2, explosionSound3, explosionSound4, explosionSound5;
    public bool explosionSoundPlaying1, explosionSoundPlaying2, explosionSoundPlaying3, explosionSoundPlaying4, explosionSoundPlaying5;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            laserSound.enabled = true;
            laserSound.Play();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepos.position, firepos.rotation);
        //bullet.GetComponent<bullet>().boomSound = explosionSound;
        bullet.transform.Rotate(0, 0, 90);
        bullet.GetComponent<bullet>().player = this.gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
    }

    public void playExplosionSound()
    {
        if (!explosionSoundPlaying1)
        {
            explosionSoundPlaying1 = true;
            explosionSound1.Play();
            StartCoroutine(wait1());
            return;
        }
        if (!explosionSoundPlaying2)
        {
            explosionSoundPlaying2 = true;
            explosionSound2.Play();
            StartCoroutine(wait2());
            return;
        }
        if (!explosionSoundPlaying3)
        {
            explosionSoundPlaying3 = true;
            explosionSound3.Play();
            StartCoroutine(wait3());
            return;
        }
        if (!explosionSoundPlaying4)
        {
            explosionSoundPlaying4 = true;
            explosionSound4.Play();
            StartCoroutine(wait4());
            return;
        }
        if (!explosionSoundPlaying5)
        {
            explosionSoundPlaying5 = true;
            explosionSound5.Play();
            StartCoroutine(wait5());
            return;
        }
    }

    IEnumerator wait1()
    {
        yield return new WaitForSecondsRealtime(1);
        explosionSoundPlaying1 = false;
    }
    IEnumerator wait2()
    {
        yield return new WaitForSecondsRealtime(1);
        explosionSoundPlaying2 = false;
    }
    IEnumerator wait3()
    {
        yield return new WaitForSecondsRealtime(1);
        explosionSoundPlaying3 = false;
    }
    IEnumerator wait4()
    {
        yield return new WaitForSecondsRealtime(1);
        explosionSoundPlaying4 = false;
    }
    IEnumerator wait5()
    {
        yield return new WaitForSecondsRealtime(1);
        explosionSoundPlaying5 = false;
    }
}
