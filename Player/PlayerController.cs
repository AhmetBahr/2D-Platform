using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //New Player Control Codes


    private float movementInputDirection;
    public float movementSpeed = 10.0f;
    public float jumpForce = 8.0f;
    public float groundCheckRadius;

    private int amountOfJumpsLeft;
    public int amountOfJumps = 2;

    private bool isFactingRight = true;
    private bool isWalking;
    public bool isGrounded;
    public bool canJump;

    public Transform groundCheck;

    private Rigidbody2D rb;
    private Animator anim;

    public LayerMask whatiIsGround;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckIfCanJump();
    }


    private void FixedUpdate()
    {
        ApplyMovment();
        CheckSurrounings();
    }

    void CheckSurrounings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatiIsGround);
    }

    private void CheckMovementDirection()
    {
        if (isFactingRight && movementInputDirection < 0)
            Flip();
        else if(!isFactingRight && movementInputDirection > 0)
            Flip();

        if(rb.velocity.x != 0)
        {
            isWalking= true;
        }
        else
        {
            isWalking= false;
        }
    }

    void UpdateAnimation()
    {
        anim.SetBool("isWalk", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }

    void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
            amountOfJumpsLeft--;
        }

    }

    void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;

        }
        else
        {
            canJump = true;
        }


    }


    void Jump()
    {

        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
          
        }
    }

    void ApplyMovment()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    void Flip()
    {
        isFactingRight = !isFactingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
