using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public string weaponName;
    public int weaponId;
    public bool weaponUnlocked; //true = useable, false = not usable yet
    public float weaponDamage;
    public float weaponAttackSpeed;
    public bool weaponMeleRanged; //true = mele, false = range
    public int ammoMax;
    public GameObject bulletPrefab;
    public AudioSource attackSound;
    public AudioSource explosionSound1, explosionSound2, explosionSound3, explosionSound4, explosionSound5;
    public Sprite UISprite;

    //private void Start()
    //{
    //    if (weaponId == 1 || weaponId == 2 || weaponId == 3 || weaponId == 4 || weaponId == 5 || weaponId == 6 || weaponId == 7)
    //    {
    //        attackSound = GameObject.Find("Sounds/laserSound").GetComponent<AudioSource>();
    //        explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
    //        explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
    //        explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
    //        explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
    //        explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
    //    }
    //    else
    //    {
    //        attackSound = GameObject.Find("Sounds/laserSound").GetComponent<AudioSource>();
    //        explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
    //        explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
    //        explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
    //        explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
    //        explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
    //    }
    //}

}
