using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject hitEffect;
    public float damage = 1f;

    private void Start()
    {

        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeHit(damage);
        }
    }

}
