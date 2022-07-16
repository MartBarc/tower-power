using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    //public Transform MoveToTransform;
    public bool isAtBase = false;
    public float hitPoints;
    public float maxHitPoints = 5;
    public HealthBar healthbar;
    //public Vector3 healthbarOffset;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public bool canAttack = true;
    public bool canbeHurt = true;
    public AudioSource attackSound;
    public float attackDistance = 1f;

    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        //healthbar.transform.position = new Vector3(0f, healthbarOffset);
        //MoveToTransform = GameObject.Find("player").transform;
        attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
    }

    public void TriggerUpdate(Transform moveTo)
    {

        if (Vector2.Distance(transform.position, moveTo.position) > attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTo.position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, moveTo.position) < attackDistance)
        {
            if (canAttack)
            {
                attackSound.Play();
                canAttack = false;
                playerMovement player = moveTo.gameObject.GetComponent<playerMovement>();
                if (player!=null)
                {
                    player.TakeHit(attackDamage);
                }
                StartCoroutine(attackCooldown());
                return;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Base")
        //{
        //    isAtBase = true;
        //}
        //isAtBase = true;
        //Debug.Log("collision name = " + collision.gameObject.name);
        if (collision.gameObject.tag == "damageEnemy")
        {
            TakeHit(collision.gameObject.GetComponent<bullet>().damage);
        }
    }

    public void TakeHit(float damage)
    {


        if (canbeHurt)
        {
            canbeHurt = false;
            StartCoroutine(damageFromPlayerCooldown());
            hitPoints -= damage;
            healthbar.SetHealth(hitPoints, maxHitPoints);
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        canAttack = true;
    }

    IEnumerator damageFromPlayerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        canbeHurt = true;
    }
}
