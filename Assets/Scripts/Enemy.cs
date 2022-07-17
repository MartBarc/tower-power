using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Bullet_Enemy projectilePrefab;
    [SerializeField] public GameObject poofPrefab;
    public Color poofColor = Color.green;

    public float speed;
    //public Transform MoveToTransform;
    public bool isAtBase = false;
    public float hitPoints;
    public float maxHitPoints = 5;
    public HealthBar healthbar;
    //public Vector3 healthbarOffset;
    public float attackDelay = 1f; //The lower the worse it gets
    public float attackDamage = 1f;
    public bool canbeHurt = true;
    public AudioSource attackSound;
    public float attackDistance = 1f;
    public bool isMeleEnemy; //if is mele = true, if is range = false
    public bool canAttack = true;
    public Transform firepos;
    public Transform Gun;
    public float bulletForce = 10f;
    public float shootDistance = 6f;
    public Animator EnemyAnimation;
    public bool isWalking = false;
    public bool isAlive = true;
    //debug
    public Transform moveTo;

    public bool waitToSpawn = true;

    public int attackType = 0; //1 is suicider



    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        //healthbar.transform.position = new Vector3(0f, healthbarOffset);
        //MoveToTransform = GameObject.Find("player").transform;
        attackSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();
        //debug
        moveTo = GameObject.Find("player").transform;

        StartCoroutine(WaitToAttack());
    }

    private void OnDestroy()
    {
        GameObject effect = Instantiate(poofPrefab, transform.position, Quaternion.identity);
        if (effect != null)
        {
            effect.GetComponent<SpriteRenderer>().color = poofColor;
            Destroy(effect, 1f);
        }
    }

    public void playAttackAnim()
    {
        EnemyAnimation.SetTrigger("EnemyMeleAttackTrig");
    }

    public void playDieAnim()
    {
        EnemyAnimation.SetTrigger("EnemyDieTrig");
    }

    private IEnumerator WaitToAttack() // go to zero
    {
        yield return new WaitForSeconds(2f);
        waitToSpawn = false;
    }

    //debug
    //public void TriggerUpdate(Transform moveTo)
    public void Update()
    {
        if (waitToSpawn)
        {
            return;
        }
        if (isAlive)
        {
            if (!isMeleEnemy && canAttack)
            {
                if (Vector2.Distance(transform.position, moveTo.position) < shootDistance)
                {
                    isWalking = false;
                    EnemyAnimation.SetBool("isWalking", false);
                    //attackSound.Play();
                    canAttack = false;
                    Player player = moveTo.gameObject.GetComponent<Player>();
                    StartCoroutine(rangeAttackCooldown(player, moveTo));
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, moveTo.position, speed * Time.deltaTime);
                    isWalking = true;
                    EnemyAnimation.SetBool("isWalking", true);
                }
                return;
            }
            if (isMeleEnemy && canAttack)
            {
                if (Vector2.Distance(transform.position, moveTo.position) < attackDistance)
                {
                    isWalking = false;
                    EnemyAnimation.SetBool("isWalking", false);
                    //attackSound.Play();
                    canAttack = false;
                    Player player = moveTo.gameObject.GetComponent<Player>();
                    StartCoroutine(meleAttackCooldown(player));
                    return;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, moveTo.position, speed * Time.deltaTime);
                    isWalking = true;
                    EnemyAnimation.SetBool("isWalking", true);
                }
            }

            Vector3 Dir = gameObject.transform.position - moveTo.position;
            if (Dir.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (Dir.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
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
                isAlive = false;
                EnemyAnimation.SetTrigger("EnemyDieTrig");
                Destroy(gameObject, 2f);
            }
        }
    }

    IEnumerator meleAttackCooldown(Player player)
    {
        yield return new WaitForSecondsRealtime(attackDelay);
        if (player != null)
        {
            playAttackAnim();
            attackSound.Play();
            player.TakeHit(attackDamage);
            if (attackType == 1)
            {
                Destroy(gameObject);
            }
        }
        yield return new WaitForSecondsRealtime(attackDelay);
        canAttack = true;
        //canShoot = true;
    }

    IEnumerator rangeAttackCooldown(Player player, Transform moveTo)
    {
        yield return new WaitForSecondsRealtime(attackDelay);
        if (isAlive)
        {
            playAttackAnim();
            attackSound.Play();
            Shoot(moveTo);
        }
        yield return new WaitForSecondsRealtime(attackDelay);
        canAttack = true;
    }

    IEnumerator damageFromPlayerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        canbeHurt = true;
    }

    public void Shoot(Transform target)
    {
        Bullet_Enemy projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
       
        Physics2D.IgnoreCollision(projectile.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); //

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
    }
}
