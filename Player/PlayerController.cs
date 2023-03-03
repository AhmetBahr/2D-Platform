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
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float arDragMultiplier = 0.95f;
    public float variableJumpHeightMultip = 0.5f;

    private int amountOfJumpsLeft;
    public int amountOfJumps = 2;

    private bool isFactingRight = true;
    private bool isWalking;
    public bool isGrounded;
    private bool isTouchingWall;
    public bool canJump;
    private bool isWallSliding;

    public Transform wallCheck;
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
        ChecIfWallSlider();
    }


    private void FixedUpdate()
    {
        ApplyMovment();
        CheckSurrounings();
    }

    void CheckSurrounings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatiIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatiIsGround);

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
        anim.SetBool("isWallSliding" , isWallSliding); // Not: Sliding animasyonu yok 
    }

    void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
            amountOfJumpsLeft--;
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultip); 
        }


    }


    void ChecIfWallSlider()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
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
      
         if (isGrounded)
         {
             rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);

         }
         else if(!isGrounded && !isWallSliding && movementInputDirection != 0)
         {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }


         }
         else if(!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(movementSpeed * arDragMultiplier, rb.velocity.y);
            
        }


        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    
    }

    void Flip()
    {
        if (!isWallSliding)
        {
            isFactingRight = !isFactingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+ wallCheckDistance,wallCheck.position.y,wallCheck.position.z));
    }

}
