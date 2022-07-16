using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shooting : MonoBehaviour
{
    public Transform firepos;
    public Transform Gun;
    public float firerate;
    public bool fireRateWaitBool = false; //if true = shooting on cd
    public float bulletForce = 20f;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetMouseButton(0))
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
        GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
        bullet.transform.Rotate(0, 0, 90);
        bullet.GetComponent<bullet>().player = this.gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
    }

    void Swing()
    {
        GameObject bullet = Instantiate(this.gameObject.GetComponent<weaponController>().bulletPrefab, firepos.position, firepos.rotation);
        bullet.GetComponent<meleAttack>().player = this.gameObject;
        bullet.GetComponent<meleAttack>().knockBack = this.gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().meleKnockback;

    }

    IEnumerator fireRateWait(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        fireRateWaitBool = false;
    }


}
