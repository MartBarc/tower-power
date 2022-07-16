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
    public float hitPoints;
    public float maxHitPoints = 5;
    public HealthBar healthbar;
    public bool isAlive = true;
    public GameObject heart1, heart2, heart3, heart4, heart5;
    public Sprite HeartEmpty, HeartHalf, HeartFull;

    Vector2 movement;
    Vector2 mousepos;

    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        heartImageHandler();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cameraBool)
            {
                FloorCamera.m_Priority = 11;
                cameraBool = false;
            }
            else
            {
                FloorCamera.m_Priority = 9;
                cameraBool = true;
            }
        }
    }

    public void TakeHit(float damage)
    {
        hitPoints -= damage;
        //healthbar.SetHealth(hitPoints, maxHitPoints);
        if (hitPoints <= 0)
        {
            //Destroy(gameObject);
            isAlive = false;
        }
        heartImageHandler();
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //gunRb.position = this.transform.position;
        gunRb.position = new Vector3(rb.position.x, rb.position.y - 0.5f, 0);
        Vector2 lookDir = mousepos - gunRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        gunRb.rotation = angle;
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
