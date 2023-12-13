using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumForce;
    private float xInput;

    [Header("Collision check")] 
    [SerializeField] float groundCheckRadius;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    private bool isGround;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


// Update is called once per frame
    void Update()
    {
        AnimationControllers();
        CollisionChecks();
        FlipController();

        xInput = Input.GetAxisRaw("Horizontal");
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void AnimationControllers()
    {
        bool isGround = rb.velocity.x != 0;
        anim.SetBool("isGrounded", isGround);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("xvelocity", rb.velocity.x);
       
    }



    private void Jump()
    {
        if (isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (mousePos.x < transform.position.x && facingRight)
            Flip();
        else if (mousePos.x > transform.position.x && !facingRight)
            Flip();
    }


private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    
    private void CollisionChecks()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }
}
