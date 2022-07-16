using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public int ammo;
    public GameObject weaponUI1, weaponUI2, weaponUI3, weaponUI4, weaponUI5, weaponUI6;
    public Sprite deafulNullSprite;
    public TextMeshProUGUI ammoText;

    [SerializeField]
    public List<GameObject> CurrentWeaponList = new List<GameObject>();

    private void Update()
    {
        if (CurrentWeaponList.Count > 0)
        {
            bulletPrefab = currentWeapon.GetComponent<WeaponData>().bulletPrefab;
            ammoMax = currentWeapon.GetComponent<WeaponData>().ammoMax;
            ammoText.text = ammo + " / " + ammoMax;
            if (ammo == 0)
            {
                //switch to another weapon
                //make a script for this
                int randomNumber = Random.Range(0, CurrentWeaponList.Count);
                currentWeapon = CurrentWeaponList[randomNumber];
                ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
                //Debug.Log("i rolled a " + randomNumber + ". count = " + CurrentWeaponList.Count);
                getWeaponSound();
                //add rolling animation here
            }
        }


        if (CurrentWeaponList.Count == 1)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI3.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if(CurrentWeaponList.Count == 2)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 3)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 4)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            weaponUI4.GetComponent<Image>().sprite = CurrentWeaponList[3].GetComponent<WeaponData>().UISprite;
            weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 5)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            weaponUI4.GetComponent<Image>().sprite = CurrentWeaponList[3].GetComponent<WeaponData>().UISprite;
            weaponUI5.GetComponent<Image>().sprite = CurrentWeaponList[4].GetComponent<WeaponData>().UISprite;
        }
        else if (CurrentWeaponList.Count >= 6)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            weaponUI4.GetComponent<Image>().sprite = CurrentWeaponList[3].GetComponent<WeaponData>().UISprite;
            weaponUI5.GetComponent<Image>().sprite = CurrentWeaponList[4].GetComponent<WeaponData>().UISprite;
            weaponUI6.GetComponent<Image>().sprite = CurrentWeaponList[5].GetComponent<WeaponData>().UISprite;
        }
        else
        {
            weaponUI1.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI2.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI3.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }


    }

    private void Start()
    {
        if (CurrentWeaponList.Count > 0)
        {
            currentWeapon = CurrentWeaponList[0];
            ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
            getWeaponSound();
        }
        else
        {
            ammo = 0;
            ammoMax = 0;
        }
    }

    public void getWeaponSound()
    {
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 1 || currentWeapon.GetComponent<WeaponData>().weaponId == 2 || 
            currentWeapon.GetComponent<WeaponData>().weaponId == 3 || currentWeapon.GetComponent<WeaponData>().weaponId == 4 || 
            currentWeapon.GetComponent<WeaponData>().weaponId == 5 || currentWeapon.GetComponent<WeaponData>().weaponId == 6 || 
            currentWeapon.GetComponent<WeaponData>().weaponId == 7)
        {
            attackSound = GameObject.Find("Sounds/laserSound").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
        }
        else
        {
            attackSound = GameObject.Find("Sounds/laserSound").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
        }
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
