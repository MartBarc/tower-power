using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject hitEffect;
    public float damage = 1f;
    public float knockBack;
    public Animator weaponAnimator;
    public float selfDestroyTime = 0.3f;

    private void Start()
    {
        Destroy(gameObject, selfDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeHit(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * knockBack);
        }
    }
    public void playBigHammerAnim()
    {
        weaponAnimator.SetTrigger("bigHammerAttackTrig");
    }
    public void playScythe1Anim()
    {
        weaponAnimator.SetTrigger("scythe1Trig");
    }
    public void playScythe2Anim()
    {
        weaponAnimator.SetTrigger("scythe2Trig");
    }
    public void playScythe3Anim()
    {
        weaponAnimator.SetTrigger("scythe3Trig");
    }

}
