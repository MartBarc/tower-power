using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject hitEffect;
    public float damage = 1f;
    //public AudioSource boomSound = null;
    public bool spin = false;
    public float spinSpeed = 3000f;

    private void Update()
    {
        if (spin)
        {
            this.gameObject.GetComponent<Rigidbody2D>().MoveRotation(this.gameObject.GetComponent<Rigidbody2D>().rotation + spinSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

        player.GetComponent<weaponController>().playExplosionSound();
        Destroy(effect, 1f);

        Destroy(gameObject);
    }
}
