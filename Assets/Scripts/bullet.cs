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
    public bool isEgg = false;
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
        if (isEgg)
        {
            int randomNumber = Random.Range(0, 14);
            if (randomNumber == 0)
            {
                GameObject chicken = Instantiate(GameObject.Find("GameController").GetComponent<GameController>().chickenPrefab,
    transform.position, Quaternion.identity);
                Destroy(chicken, 5f);
            }

        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            player.GetComponent<weaponController>().playExplosionSound();
            Destroy(effect, 1f);
        }
        //player.GetComponent<weaponController>().playExplosionSound();
        //Destroy(effect, 1f);

        Destroy(gameObject);
    }
}
