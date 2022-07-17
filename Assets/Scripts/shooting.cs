using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public Transform firepos;
    //public Transform meleFirePos;
    public Transform Gun;
    public float firerate;
    public bool fireRateWaitBool = false; //if true = shooting on cd
    public float bulletForce = 20f;
    public bool ShootingEnabled = true;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetMouseButton(0)) && ShootingEnabled)
        {
            if (!fireRateWaitBool && this.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count > 0)
            {
                if (this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().weaponMeleRanged) //true = mele
                {
                    firerate = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().firerate;
                    fireRateWaitBool = true;
                    StartCoroutine(fireRateWait(firerate));
                    this.gameObject.GetComponent<weaponController>().updateAmmo();
                    Swing();
                    this.gameObject.GetComponent<Player>().gunImage.SetActive(false);
                    this.gameObject.GetComponent<weaponController>().attackSound.enabled = true;
                    this.gameObject.GetComponent<weaponController>().attackSound.Play();
                    this.gameObject.GetComponent<weaponController>().ammo--;
                    this.gameObject.GetComponent<weaponController>().updateAmmo();
                }
                else
                {
                    firerate = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().firerate;
                    fireRateWaitBool = true;
                    StartCoroutine(fireRateWait(firerate));
                    this.gameObject.GetComponent<weaponController>().updateAmmo();
                    Shoot();
                    
                    this.gameObject.GetComponent<weaponController>().attackSound.enabled = true;
                    this.gameObject.GetComponent<weaponController>().attackSound.Play();
                    this.gameObject.GetComponent<weaponController>().ammo--;
                    this.gameObject.GetComponent<weaponController>().updateAmmo();
                }
            }
        }
    }

    void Shoot()
    {
        int weaponIdLocal = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().weaponId;
        //GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
        if (weaponIdLocal == 13 || weaponIdLocal == 7)
        {
            this.gameObject.GetComponent<Player>().gunImage.SetActive(false);
            GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
            bullet.transform.Rotate(0, 0, 90);
            bullet.GetComponent<bullet>().player = this.gameObject;
            bullet.GetComponent<bullet>().spin = true;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (weaponIdLocal == 12 || weaponIdLocal == 0 || weaponIdLocal == 4)
        {
            GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
            bullet.transform.Rotate(0, 0, 0);
            bullet.GetComponent<bullet>().player = this.gameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (weaponIdLocal == 1)
        {
            GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
            bullet.transform.Rotate(0, 0, 90);
            bullet.GetComponent<bullet>().player = this.gameObject;
            bullet.GetComponent<bullet>().isEgg = true;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (weaponIdLocal == 2)
        {
            int randomNumber = Random.Range(0, 3);
            if (randomNumber == 0)
            {
                GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<cardGunScript>().bulletPrefab1, firepos.position, firepos.rotation);
                bullet.GetComponent<bullet>().player = this.gameObject;
                bullet.GetComponent<bullet>().spin = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
            }
            if (randomNumber == 1)
            {
                GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<cardGunScript>().bulletPrefab2, firepos.position, firepos.rotation);
                bullet.GetComponent<bullet>().player = this.gameObject;
                bullet.GetComponent<bullet>().spin = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
            }
            if (randomNumber == 2)
            {
                GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<cardGunScript>().bulletPrefab3, firepos.position, firepos.rotation);
                bullet.GetComponent<bullet>().player = this.gameObject;
                bullet.GetComponent<bullet>().spin = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
            }
            if (randomNumber == 3)
            {
                GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<cardGunScript>().bulletPrefab4, firepos.position, firepos.rotation);
                bullet.GetComponent<bullet>().player = this.gameObject;
                bullet.GetComponent<bullet>().spin = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
            bullet.transform.Rotate(0, 0, 90);
            bullet.GetComponent<bullet>().player = this.gameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    void Swing()
    {
        GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
        bullet.GetComponent<meleAttack>().player = this.gameObject;
        bullet.GetComponent<meleAttack>().knockBack = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().meleKnockback;

        //
        int weaponIdLocal = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().weaponId;
        if (weaponIdLocal == 5 || weaponIdLocal == 3 || weaponIdLocal == 10 || weaponIdLocal == 7)  //swing anim
        {
            GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playSwingAnim();
            //bullet.GetComponent<meleAttack>().playSwingAnim();
        }
        if (weaponIdLocal == 11)    //spear stab anim
        {
            GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playSpearStabAnim();
            //bullet.GetComponent<meleAttack>().playSpearStabAnim();
        }
        if (weaponIdLocal == 15)    //dagger stab anim
        {
            GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playDaggerStabAnim();
            //bullet.GetComponent<meleAttack>().playDaggerStabAnim();
        }
        if (weaponIdLocal == 8)     //greatsword swing anim
        {
            GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playSwingBigAnim();
            //bullet.GetComponent<meleAttack>().playSwingBigAnim();
        }
        if (weaponIdLocal == 9)     //hammer swing anim
        {
            //GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playBigHammerAnim();
            bullet.GetComponent<meleAttack>().playBigHammerAnim();
        }
        if (weaponIdLocal == 6)     //scythe anim
        {
            //GameObject.Find("player/gun/firepos").GetComponent<meleAttackAnimations>().playBigHammerAnim();
            if (this.gameObject.GetComponent<weaponController>().bulletPrefab.name == "scythebullet1")
            {
                bullet.GetComponent<meleAttack>().playScythe1Anim();
            }
            if (this.gameObject.GetComponent<weaponController>().bulletPrefab.name == "scythebullet2")
            {
                bullet.GetComponent<meleAttack>().playScythe2Anim();
            }
            if (this.gameObject.GetComponent<weaponController>().bulletPrefab.name == "scythebullet3")
            {
                bullet.GetComponent<meleAttack>().playScythe3Anim();
            }
        }

    }

    IEnumerator fireRateWait(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        fireRateWaitBool = false;
        this.gameObject.GetComponent<Player>().gunImage.SetActive(true);
    }


}
