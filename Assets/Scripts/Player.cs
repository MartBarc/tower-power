using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Rigidbody2D gunRb;
    public GameObject gunImage;
    public CinemachineVirtualCamera FloorCamera;
    private bool cameraBool = false;
    public int hitPoints;
    public int maxHitPoints = 10;
    public HealthBar healthbar;
    public bool isAlive = true;
    public GameObject heart1, heart2, heart3, heart4, heart5;
    public Sprite HeartEmpty, HeartHalf, HeartFull;
    public bool isFacingRight = true;
    public bool rollCooldownBool = true;
    public float rollCoolDown = 5f;

    [SerializeField] public GameObject poof;
    [SerializeField] public GameObject gameOverText;
    private GameObject gameover;

    Vector2 movement;
    Vector2 mousepos;

    [SerializeField] public GameObject playerWeap;

    public bool pause = false;

    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        heartImageHandler();
    }

    private void OnDestroy()
    {

        //GameObject effect = Instantiate(poof, transform.position, Quaternion.identity);
        //if (effect != null)
        //{
        //    effect.GetComponent<SpriteRenderer>().color = Color.white;
        //    Destroy(effect, 1f);
        //}
        StartCoroutine(playerDied());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        { return; }

        if (pause)
            return;

        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x > 0)
        {
            isFacingRight = true;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (movement.x < 0)
        {
            isFacingRight = false;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        animator.SetFloat("horizontal", movement.x);
        //animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (cameraBool)
        //    {
        //        FloorCamera.m_Priority = 11;
        //        cameraBool = false;
        //    }
        //    else
        //    {
        //        FloorCamera.m_Priority = 9;
        //        cameraBool = true;
        //    }
        //}

        if ((Input.GetButtonDown("Fire2") || Input.GetMouseButton(1)) && rollCooldownBool)
        {
            //roll animation
            rollCooldownBool = false;
            animator.SetTrigger("playerRollTrig");

            StartCoroutine(rollAbility());
            StartCoroutine(rollCoolDownWait(rollCoolDown));
        }
    }

    IEnumerator playerDied()
    {
        animator.SetTrigger("playerDied");
        playerWeap.SetActive(false);

        //tutObj init spawn here...
        gameover = Instantiate(gameOverText, this.transform.position, Quaternion.identity);
        //Vector3 oLoc = gameover.transform.position;
        ////gameover.transform.parent = transform;
        //gameover.transform.position = new Vector3(transform.position.x, 10f);

        //float speed = 50f;
        //float rot = 0f;
        //while (transform.position != oLoc)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, rot);
        //    rot += 10f;
        //    transform.position = Vector2.MoveTowards(transform.position, oLoc, speed * Time.deltaTime);
        //    yield return new WaitForSeconds(0.01f);
        //}
        //GameObject effect = Instantiate(poof, transform.position, Quaternion.identity);
        //if (effect != null)
        //{
        //    Destroy(effect, 1f);
        //}
        //transform.eulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSecondsRealtime(5f);

        gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        Application.Quit();
    }

    IEnumerator rollCoolDownWait(float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
        rollCooldownBool = true;
    }

    IEnumerator rollAbility()
    {
        gameObject.GetComponent<weaponController>().ammo = 0;
        moveSpeed = moveSpeed * 2.0f;
        yield return new WaitForSecondsRealtime(0.5f);
        moveSpeed = 5f;
    }

    public void TakeHit(int damage)
    {
        hitPoints -= damage;
        //healthbar.SetHealth(hitPoints, maxHitPoints);
        if (hitPoints <= 0 && isAlive)
        {
            StartCoroutine(playerDied());
            isAlive = false;
            hitPoints = 0;
            heartImageHandler();
        }
        heartImageHandler();
    }

    void FixedUpdate()
    {
        if (!isAlive)
        { return;  }


        if (pause)
            return;

        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //gunRb.position = this.transform.position;
        if (isFacingRight)
        {
            gunRb.position = new Vector3(rb.position.x + 0.25f, rb.position.y - 0.2f, 0);
        }
        else
        {
            gunRb.position = new Vector3(rb.position.x - 0.25f, rb.position.y - 0.2f, 0);
        }
        if (!gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().weaponMeleRanged) 
        {
            //ranged
            if (gameObject.GetComponent<weaponController>().currentWeapon.GetComponent<WeaponData>().weaponId == 99)
            {
                Vector2 lookDir = mousepos - gunRb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                gunRb.rotation = angle;
            }
            else
            {
                Vector2 lookDir = mousepos - gunRb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                gunRb.rotation = angle;
            }

        }
        else
        {
            //mele
            Vector2 lookDir = mousepos - gunRb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            gunRb.rotation = angle;

            GameObject.Find("player/gun/gunImage").transform.position = gunRb.position;
            GameObject.Find("player/gun/gunImage").transform.rotation = Quaternion.identity;
        }

    }

    public void heartImageHandler()
    {
        if (hitPoints == 0)
        {
            heart1.GetComponent<Image>().sprite = HeartEmpty;
            heart2.GetComponent<Image>().sprite = HeartEmpty;
            heart3.GetComponent<Image>().sprite = HeartEmpty;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 1)
        {
            heart1.GetComponent<Image>().sprite = HeartHalf;
            heart2.GetComponent<Image>().sprite = HeartEmpty;
            heart3.GetComponent<Image>().sprite = HeartEmpty;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 2)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartEmpty;
            heart3.GetComponent<Image>().sprite = HeartEmpty;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 3)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartHalf;
            heart3.GetComponent<Image>().sprite = HeartEmpty;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 4)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartEmpty;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 5)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartHalf;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 6)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartFull;
            heart4.GetComponent<Image>().sprite = HeartEmpty;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 7)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartFull;
            heart4.GetComponent<Image>().sprite = HeartHalf;
            heart5.GetComponent<Image>().sprite = HeartEmpty;
        }
        if (hitPoints == 8)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartFull;
            heart4.GetComponent<Image>().sprite = HeartFull;
            heart5.GetComponent<Image>().sprite = HeartEmpty;

        }
        if (hitPoints == 9)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartFull;
            heart4.GetComponent<Image>().sprite = HeartFull;
            heart5.GetComponent<Image>().sprite = HeartHalf;

        }
        if (hitPoints == 10)
        {
            heart1.GetComponent<Image>().sprite = HeartFull;
            heart2.GetComponent<Image>().sprite = HeartFull;
            heart3.GetComponent<Image>().sprite = HeartFull;
            heart4.GetComponent<Image>().sprite = HeartFull;
            heart5.GetComponent<Image>().sprite = HeartFull;

        }
    }
}
