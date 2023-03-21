using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace playerCont
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
 

        [Header("Coyote Time")]
        [SerializeField] private float coyoteTime; 
        private float coyoteCounter;

        [Header("Multiple Jump")]
        [SerializeField] private int extraJyump;
        private int jumpCounter;

        [Header("Wall Jumping")]
        [SerializeField] private float wallJumpX; 
        [SerializeField] private float wallJumpY;

        [Header("Layer")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;


        [Header("Dash")]
        [SerializeField] private float DashForce;
        [SerializeField] private float DashCoolDown;
        [SerializeField] private float StartDashTimer;
        private float currentDashTimer;
        private bool isDashing;



        private Rigidbody2D body;
        private Animator anim;
        private BoxCollider2D boxCollider;
        private float wallJumpCooldown;
        private float horizontalInput;
        private float cooldownTimer = Mathf.Infinity;

        private void Awake()
        { 
            //Refernas
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();   
            boxCollider= GetComponent<BoxCollider2D>();
      

        }

        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //Karakteri sag sola dondurmek icin
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);



            //Animator parametreleri
            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", isGrounded());

            //Ziplama iþlemleri
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
                body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

            if (onWall())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                if (isGrounded())
                {
                    coyoteCounter = coyoteTime;
                    jumpCounter = extraJyump;
                }
                else
                {
                    coyoteCounter -= Time.deltaTime;
                }
            }

            //Dash
            if (Input.GetKeyDown(KeyCode.E) && cooldownTimer > DashCoolDown)
                Dash();

            cooldownTimer += Time.deltaTime;

            if (isDashing)
            {
                if (horizontalInput > 0.01f)
                    body.velocity = Vector2.right * DashForce;
                else if (horizontalInput < -0.01f)
                    body.velocity = Vector2.left * DashForce;

                currentDashTimer -= Time.deltaTime;
                if (currentDashTimer <= 0)
                {
                    isDashing = false;
                }

            }

        }

        private void Dash()
        {
            isDashing = true;
            currentDashTimer = StartDashTimer;
            cooldownTimer = 0;
        }

        private void Jump()
        {
            if (coyoteCounter <= 0 && !onWall() && jumpCounter <=0) return;

            if (onWall())
                WallJump();
            else
            {
                if (isGrounded())
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (coyoteCounter > 0)
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                    else
                    {
                        if (jumpCounter > 0)
                        {
                            body.velocity = new Vector2(body.velocity.x, jumpPower);
                            jumpCounter--;
                        }
                    }
                }
                coyoteCounter = 0;   
            }

        }

        private void WallJump()
        {
            body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
            wallJumpCooldown = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
          
        }

        private bool isGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0, Vector2.down, 0.1f,groundLayer);


            return raycastHit.collider != null;
        }
        private bool onWall()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
            return raycastHit.collider != null;
        }



        public bool canAttack()
        {
            return horizontalInput == 0 && isGrounded() && !onWall();
        }

    }

}