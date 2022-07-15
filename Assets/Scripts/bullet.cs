using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject hitEffect;
    public float damage = 1f;
    //public AudioSource boomSound = null;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //effect.GetComponent<playSound>().Sound = boomSound;
        //effect.GetComponent<playSound>().playSound1();
        player.GetComponent<shooting>().playExplosionSound();
        Destroy(effect, 1f);
        
        Destroy(gameObject);
    }
}
