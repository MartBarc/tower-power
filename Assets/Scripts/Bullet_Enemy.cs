using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    //public GameObject player;
    public GameObject hitEffect;
    public float damage = 1f;
    //public AudioSource boomSound = null;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.collider.name);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (effect != null)
            {
                effect.GetComponent<SpriteRenderer>().color = Color.green;
                Destroy(effect, 1f);
            }
            collision.gameObject.GetComponent<Enemy>().TakeHit(damage);
            Destroy(gameObject);
            //Collider2D d = GetComponent<Collider2D>();
            //Physics2D.IgnoreCollision(collision.collider, d,true);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.collider.name);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (effect != null)
            {
                effect.GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(effect, 1f);
            }
            collision.gameObject.GetComponent<Player>().TakeHit(damage);
            Destroy(gameObject);
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //collision.gameObject.GetComponent<pla>
        //player.GetComponent<weaponController>().playExplosionSound();
        //Destroy(effect, 1f);

        //Destroy(gameObject);
    }
}
