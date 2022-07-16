using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Bullet_Enemy projectilePrefab;

    public float speed;
    //public Transform MoveToTransform;
    public bool isAtBase = false;
    public float hitPoints;
    public float maxHitPoints = 5;
    public HealthBar healthbar;
    //public Vector3 healthbarOffset;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public bool canMelee = true;
    public bool canbeHurt = true;
    public AudioSource attackSound;
    public float attackDistance = 1f;

    public bool canShoot = false;
    public Transform firepos;
    public Transform Gun;
    public float bulletForce = 10f;
    public float shootDistance = 6f;


    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        //healthbar.transform.position = new Vector3(0f, healthbarOffset);
        //MoveToTransform = GameObject.Find("player").transform;
        attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();

        ExampleCoroutine();
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void TriggerUpdate(Transform moveTo)
    {

        if (canShoot)
        {
            if (Vector2.Distance(transform.position, moveTo.position) < shootDistance)
            {
                canShoot = false;
                //Shoot(moveTo);
                Player player = moveTo.gameObject.GetComponent<Player>();
                StartCoroutine(rangeAttackCooldown(player, moveTo));
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveTo.position, speed * Time.deltaTime);
            }
            return;
        }
        else if (Vector2.Distance(transform.position, moveTo.position) < attackDistance)
        {
            if (canMelee)
            {
                attackSound.Play();
                canMelee = false;
                Player player = moveTo.gameObject.GetComponent<Player>();
                //if (player!=null)
                //{
                //    player.TakeHit(attackDamage);
                //}
                StartCoroutine(meleAttackCooldown(player));
                return;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTo.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "damageEnemy")
        {
            TakeHit(collision.gameObject.GetComponent<bullet>().damage);
        }
        else if (collision.gameObject.tag == "damagePlayer")
        {
            Debug.Log(collision.collider.name);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        //if (collision.gameObject.name == "Base")
        //{
        //    isAtBase = true;
        //}
        //isAtBase = true;
        //Debug.Log("collision name = " + collision.gameObject.name);
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

    IEnumerator meleAttackCooldown(Player player)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (player != null)
        {
            attackSound.Play();
            player.TakeHit(attackDamage);
        }
        yield return new WaitForSecondsRealtime(1);
        canMelee = true;
        //canShoot = true;
    }

    IEnumerator rangeAttackCooldown(Player player, Transform moveTo)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        //if (player != null)
        //{
        //    attackSound.Play();
        //    player.TakeHit(attackDamage);
        //}
        Shoot(moveTo);
        yield return new WaitForSecondsRealtime(1);
        //canMelee = true;
        canShoot = true;
    }

    IEnumerator damageFromPlayerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        canbeHurt = true;
    }

    public void Shoot(Transform target)
    {
        Bullet_Enemy bullet = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);// firepos.position, firepos.rotation); ;

        Physics2D.IgnoreCollision(bullet.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //bullet.transform.Rotate(0, 0, 90);
        //bullet.GetComponent<bullet>().player = this.gameObject;
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
    }
}
