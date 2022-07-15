using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Rigidbody2D gunRb;
    public CinemachineVirtualCamera FloorCamera;
    private bool cameraBool = false;

    Vector2 movement;
    Vector2 mousepos;

    //hey  chris what up???/
    //type something idc
    // What am I here for?

    // TO DIE!!!!!

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
}
