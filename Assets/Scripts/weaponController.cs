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
    //add reload
    //add firerate

    public GameObject currentWeapon;
    public GameObject bulletPrefab;
    public AudioSource attackSound;
    public AudioSource explosionSound1, explosionSound2, explosionSound3, explosionSound4, explosionSound5;
    public AudioSource hitSound1, hitSound2, hitSound3, hitSound4, hitSound5;
    public AudioSource weaponPoofSound;
    public bool explosionSoundPlaying1, explosionSoundPlaying2, explosionSoundPlaying3, explosionSoundPlaying4, explosionSoundPlaying5;
    public bool reloading;
    public int ammoMax;
    public int ammo;
    public GameObject weaponUI1, weaponUI2, weaponUI3, weaponUI4, weaponUI5, weaponUI6;
    public GameObject weaponUIHighlight1, weaponUIHighlight2, weaponUIHighlight3, weaponUIHighlight4, weaponUIHighlight5, weaponUIHighlight6;
    public GameObject weaponRemoveUI1, weaponRemoveUI2, weaponRemoveUI3;
    public Sprite deafulNullSprite;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI currentWeaponText;
    public int slotToRemove1, slotToRemove2;
    public WeaponData newWeapon;
    public GameObject weaponPoofPrefab;
    public GameObject ammoDiceRoll;
    public GameObject newWeaponDiceRoll;

    [SerializeField]
    public List<GameObject> CurrentWeaponList = new List<GameObject>();

    private void Update()
    {
        if (CurrentWeaponList.Count > 0)
        {
            if (ammo == 0)
            {
                reloading = true;
                StartCoroutine(reloadwait());
                //switch to another weapon
                //make a script for this
                int randomNumber = Random.Range(0, CurrentWeaponList.Count);
                currentWeapon = CurrentWeaponList[randomNumber];
                if (currentWeapon.name == "scytheGun")
                {
                    if (GameObject.Find("GameController").GetComponent<GameController>().scytheKills > 10)
                    {
                        if (GameObject.Find("GameController").GetComponent<GameController>().scytheKills > 20)
                        {
                            //make scythe be scythe 3
                            currentWeapon.GetComponent<WeaponData>().weaponDamage = currentWeapon.GetComponent<scytheScript>().scythe3Damage;
                            currentWeapon.GetComponent<WeaponData>().bulletPrefab = currentWeapon.GetComponent<scytheScript>().scythePrefab3;
                            currentWeapon.GetComponent<WeaponData>().UISprite = currentWeapon.GetComponent<scytheScript>().UISprite3;
                            currentWeapon.GetComponent<WeaponData>().inHandSprite = currentWeapon.GetComponent<scytheScript>().inHandSprite3;
                        }
                        else
                        {
                            //make scythe be scythe 2
                            currentWeapon.GetComponent<WeaponData>().weaponDamage = currentWeapon.GetComponent<scytheScript>().scythe2Damage;
                            currentWeapon.GetComponent<WeaponData>().bulletPrefab = currentWeapon.GetComponent<scytheScript>().scythePrefab2;
                            currentWeapon.GetComponent<WeaponData>().UISprite = currentWeapon.GetComponent<scytheScript>().UISprite2;
                            currentWeapon.GetComponent<WeaponData>().inHandSprite = currentWeapon.GetComponent<scytheScript>().inHandSprite2;
                        }
                    }
                    else
                    {
                        //make scythe be scythe 1
                        currentWeapon.GetComponent<WeaponData>().weaponDamage = currentWeapon.GetComponent<scytheScript>().scythe1Damage;
                        currentWeapon.GetComponent<WeaponData>().bulletPrefab = currentWeapon.GetComponent<scytheScript>().scythePrefab1;
                        currentWeapon.GetComponent<WeaponData>().UISprite = currentWeapon.GetComponent<scytheScript>().UISprite1;
                        currentWeapon.GetComponent<WeaponData>().inHandSprite = currentWeapon.GetComponent<scytheScript>().inHandSprite1;
                    }
                    updateUISprites();
                }
                ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
                this.gameObject.GetComponent<Player>().gunImage.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<WeaponData>().inHandSprite;
                //Debug.Log("i rolled a " + randomNumber + ". count = " + CurrentWeaponList.Count);
                getWeaponSound();
                //add rolling animation here
                GameObject effect = Instantiate(weaponPoofPrefab, transform.position, Quaternion.identity);
                weaponPoofSound.Play();
                Destroy(effect, 1f);

                diceRoller roller = ammoDiceRoll.GetComponent<diceRoller>();
                roller.triggerDiceRoll();

                randomNumber += 1;
                updateBorderUI(randomNumber);
                currentWeaponText.text = "" + randomNumber;
                updateAmmo();
            }
        }
        //updateUISprites();
    }

    public void updateAmmo()
    {

        bulletPrefab = currentWeapon.GetComponent<WeaponData>().bulletPrefab;
        if (bulletPrefab.GetComponent<bullet>())
        {
            bulletPrefab.GetComponent<bullet>().damage = currentWeapon.GetComponent<WeaponData>().weaponDamage;
        }
        if (bulletPrefab.GetComponent<meleAttack>())
        {
            bulletPrefab.GetComponent<meleAttack>().damage = currentWeapon.GetComponent<WeaponData>().weaponDamage;
        }
        Debug.Log("damage = " + bulletPrefab.GetComponent<meleAttack>().damage);

        ammoMax = currentWeapon.GetComponent<WeaponData>().ammoMax;
        ammoText.text = ammo + " / " + ammoMax;
        GameObject.Find("crossHair").GetComponent<cursorFollow>().setCrossHair(currentWeapon.GetComponent<WeaponData>().CrossHair);
    }

    public void updateUISprites() 
    {
        if (CurrentWeaponList.Count == 1)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            //weaponUI2.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI3.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 2)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            //weaponUI3.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 3)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            //weaponUI4.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
        }
        else if (CurrentWeaponList.Count == 4)
        {
            weaponUI1.GetComponent<Image>().sprite = CurrentWeaponList[0].GetComponent<WeaponData>().UISprite;
            weaponUI2.GetComponent<Image>().sprite = CurrentWeaponList[1].GetComponent<WeaponData>().UISprite;
            weaponUI3.GetComponent<Image>().sprite = CurrentWeaponList[2].GetComponent<WeaponData>().UISprite;
            weaponUI4.GetComponent<Image>().sprite = CurrentWeaponList[3].GetComponent<WeaponData>().UISprite;
            //weaponUI5.GetComponent<Image>().sprite = deafulNullSprite;
            //weaponUI6.GetComponent<Image>().sprite = deafulNullSprite;
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
    }


    public void updateBorderUI(int newBorder)
    {
        if (newBorder == 1)
        {
            weaponUIHighlight1.SetActive(true);
            weaponUIHighlight2.SetActive(false);
            weaponUIHighlight3.SetActive(false);
            weaponUIHighlight4.SetActive(false);
            weaponUIHighlight5.SetActive(false);
            weaponUIHighlight6.SetActive(false);
        }
        else if (newBorder == 2)
        {
            weaponUIHighlight1.SetActive(false);
            weaponUIHighlight2.SetActive(true);
            weaponUIHighlight3.SetActive(false);
            weaponUIHighlight4.SetActive(false);
            weaponUIHighlight5.SetActive(false);
            weaponUIHighlight6.SetActive(false);
        }
        else if (newBorder == 3)
        {
            weaponUIHighlight1.SetActive(false);
            weaponUIHighlight2.SetActive(false);
            weaponUIHighlight3.SetActive(true);
            weaponUIHighlight4.SetActive(false);
            weaponUIHighlight5.SetActive(false);
            weaponUIHighlight6.SetActive(false);
        }
        else if (newBorder == 4)
        {
            weaponUIHighlight1.SetActive(false);
            weaponUIHighlight2.SetActive(false);
            weaponUIHighlight3.SetActive(false);
            weaponUIHighlight4.SetActive(true);
            weaponUIHighlight5.SetActive(false);
            weaponUIHighlight6.SetActive(false);
        }
        else if (newBorder == 5)
        {
            weaponUIHighlight1.SetActive(false);
            weaponUIHighlight2.SetActive(false);
            weaponUIHighlight3.SetActive(false);
            weaponUIHighlight4.SetActive(false);
            weaponUIHighlight5.SetActive(true);
            weaponUIHighlight6.SetActive(false);
        }
        else if (newBorder >= 6)
        {
            weaponUIHighlight1.SetActive(false);
            weaponUIHighlight2.SetActive(false);
            weaponUIHighlight3.SetActive(false);
            weaponUIHighlight4.SetActive(false);
            weaponUIHighlight5.SetActive(false);
            weaponUIHighlight6.SetActive(true);
        }
    }

    private void Start()
    {
        if (CurrentWeaponList.Count > 0)
        {
            currentWeapon = CurrentWeaponList[0];
            weaponUIHighlight1.SetActive(true);
            ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
            this.gameObject.GetComponent<Player>().gunImage.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<WeaponData>().inHandSprite;
            getWeaponSound();
            currentWeaponText.text = "1";
        }
        else
        {
            ammo = 0;
            ammoMax = 0;
            currentWeaponText.text = "1";
            this.gameObject.GetComponent<Player>().gunImage.GetComponent<SpriteRenderer>().sprite = null;
        }
        weaponPoofSound = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
        updateAmmo();
        updateUISprites();
    }

    public void getWeaponSound()
    {

        if (currentWeapon.GetComponent<WeaponData>().weaponId == 0) //laser
        {
            attackSound = GameObject.Find("Sounds/laserSound").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/explosionSound (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 1) //egg
        {
            attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/enemyAttackNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/enemyAttackNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/enemyAttackNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/enemyAttackNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/enemyAttackNoise (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 2) //card
        {
            attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/enemyAttackNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/enemyAttackNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/enemyAttackNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/enemyAttackNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/enemyAttackNoise (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 3) //pot
        {
            attackSound = GameObject.Find("Sounds/potAttackNoise").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 4) //fireball
        {
            attackSound = GameObject.Find("Sounds/fireballNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/explosionSound").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/explosionSound (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/explosionSound (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/explosionSound (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/explosionSound (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/explosionSound (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 5) //sword
        {
            attackSound = GameObject.Find("Sounds/skeleAttackNoise").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 6) //scythe
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().scytheKills > 10)
            {
                if (GameObject.Find("GameController").GetComponent<GameController>().scytheKills > 20)
                {
                    //3
                    attackSound = GameObject.Find("Sounds/towerLaser").GetComponent<AudioSource>();
                }
                else
                {
                    //2
                    attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
                }
            }
            else
            {
                //1
                attackSound = GameObject.Find("Sounds/skeleAttackNoise").GetComponent<AudioSource>();
            }
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 7) //throwing axe
        {
            attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/woodNoise").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/woodNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/woodNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/woodNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/woodNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/woodNoise (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 8) //greatsword
        {
            attackSound = GameObject.Find("Sounds/skeleAttackNoiseLouder").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 9) //hammer
        {
            attackSound = GameObject.Find("Sounds/pillowNoise").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 10) //cleaver
        {
            attackSound = GameObject.Find("Sounds/knifeNoise").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 11) //spear
        {
            attackSound = GameObject.Find("Sounds/knifeNoise").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 12) //crossbow
        {
            attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/woodNoise").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/woodNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/woodNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/woodNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/woodNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/woodNoise (5)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 13) //sure you can
        {
            attackSound = GameObject.Find("Sounds/knifeNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/knifeNoise (5)").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/knifeNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/knifeNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/knifeNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/knifeNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/knifeNoise (6)").GetComponent<AudioSource>();
        }
        if (currentWeapon.GetComponent<WeaponData>().weaponId == 14) //slingshot
        {
            attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
            explosionSound1 = GameObject.Find("Sounds/woodNoise").GetComponent<AudioSource>();
            explosionSound2 = GameObject.Find("Sounds/woodNoise (1)").GetComponent<AudioSource>();
            explosionSound3 = GameObject.Find("Sounds/woodNoise (2)").GetComponent<AudioSource>();
            explosionSound4 = GameObject.Find("Sounds/woodNoise (3)").GetComponent<AudioSource>();
            explosionSound5 = GameObject.Find("Sounds/woodNoise (4)").GetComponent<AudioSource>();
            hitSound1 = GameObject.Find("Sounds/woodNoise (6)").GetComponent<AudioSource>();
        }

    }

    public void newWeaponDiceAnimation() 
    {
        if (newWeaponDiceRoll)
        {
            //Debug.Log("ERR: MISSING NEW DICE ROLL");
            //return;
        }
        newWeaponDiceRoll.SetActive(true);
        diceRoller roller = newWeaponDiceRoll.GetComponent<diceRoller>();
        roller.triggerDiceRoll();
        StartCoroutine(rollAnimationDelay());
    }


    public void chooseNewWeapon(int weaponSlotToRemove)
    {
        //turn off shooting until player gets a new item
        //this.gameObject.GetComponent<shooting>().ShootingEnabled = false;
        int myInt;
        int.TryParse(currentWeaponText.text, out myInt);
        myInt = myInt - 1;
        if (weaponSlotToRemove == 0)
        {
            CurrentWeaponList[slotToRemove1] = newWeapon.gameObject;
            if (myInt == slotToRemove1)
            {
                currentWeapon = CurrentWeaponList[slotToRemove1];
                ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
                this.gameObject.GetComponent<Player>().gunImage.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<WeaponData>().inHandSprite;
                getWeaponSound();
                updateAmmo();
            }
        }
        if (weaponSlotToRemove == 1)
        {
            CurrentWeaponList[slotToRemove2] = newWeapon.gameObject;
            if (myInt == slotToRemove2)
            {
                currentWeapon = CurrentWeaponList[slotToRemove2];
                ammo = currentWeapon.GetComponent<WeaponData>().ammoMax;
                this.gameObject.GetComponent<Player>().gunImage.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<WeaponData>().inHandSprite;
                getWeaponSound();
                updateAmmo();
            }
        }

        updateUISprites();
        weaponRemoveUI1.SetActive(false);
        weaponRemoveUI2.SetActive(false);
        weaponRemoveUI3.SetActive(false);
        this.gameObject.GetComponent<shooting>().ShootingEnabled = true;
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

    public void playHitSound()
    {
        hitSound1.Play();
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

    IEnumerator reloadwait()
    {
        yield return new WaitForSecondsRealtime(1);
        reloading = false;
    }

    IEnumerator rollAnimationDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        newWeaponDiceRoll.SetActive(false);
    }

}
