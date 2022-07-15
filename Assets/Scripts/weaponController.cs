using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    //what is current weapon
    //show current weapon sprite
    //show current weapon ammo and ammomax
    //show correct weapon
    //switch to next weapon correctly //rng
    public GameObject currentWeapon;
    public GameObject bulletPrefab;
    public AudioSource attackSound;
    public AudioSource explosionSound1, explosionSound2, explosionSound3, explosionSound4, explosionSound5;
    public bool explosionSoundPlaying1, explosionSoundPlaying2, explosionSoundPlaying3, explosionSoundPlaying4, explosionSoundPlaying5;
    public int ammoMax;
    public GameObject weaponUI1, weaponUI2, weaponUI3, weaponUI4, weaponUI5, weaponUI6;


    [SerializeField]
    public static List<WeaponData> CurrentWeaponList = new List<WeaponData>();

    private void Update()
    {
        bulletPrefab = currentWeapon.GetComponent<WeaponData>().bulletPrefab;
        attackSound = currentWeapon.GetComponent<WeaponData>().attackSound;
        explosionSound1 = currentWeapon.GetComponent<WeaponData>().explosionSound1;
        explosionSound2 = currentWeapon.GetComponent<WeaponData>().explosionSound2;
        explosionSound3 = currentWeapon.GetComponent<WeaponData>().explosionSound3;
        explosionSound4 = currentWeapon.GetComponent<WeaponData>().explosionSound4;
        explosionSound5 = currentWeapon.GetComponent<WeaponData>().explosionSound5;
        ammoMax = currentWeapon.GetComponent<WeaponData>().ammoMax;

    }

    public int playExplosionSound()
    {
        if (!explosionSoundPlaying1)
        {
            explosionSoundPlaying1 = true;
            explosionSound1.Play();
            StartCoroutine(wait1());
            return 0;
        }
        if (!explosionSoundPlaying2)
        {
            explosionSoundPlaying2 = true;
            explosionSound2.Play();
            StartCoroutine(wait2());
            return 0;
        }
        if (!explosionSoundPlaying3)
        {
            explosionSoundPlaying3 = true;
            explosionSound3.Play();
            StartCoroutine(wait3());
            return 0;
        }
        if (!explosionSoundPlaying4)
        {
            explosionSoundPlaying4 = true;
            explosionSound4.Play();
            StartCoroutine(wait4());
            return 0;
        }
        if (!explosionSoundPlaying5)
        {
            explosionSoundPlaying5 = true;
            explosionSound5.Play();
            StartCoroutine(wait5());
            return 0;
        }
        return 0;
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
