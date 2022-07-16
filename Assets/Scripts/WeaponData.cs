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


}
