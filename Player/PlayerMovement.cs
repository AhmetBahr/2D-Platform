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


        [Header("Layer")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;


        private Rigidbody2D body;
        private Animator anim;
        private BoxCollider2D boxCollider;
        private float wallJumpCooldown;
        private float horizontalInput;

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
            body.velocity = new Vector2( horizontalInput * speed, body.velocity.y);

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
                body.velocity = new Vector2(body.velocity.x, body.velocity.y/2);

            if (onWall())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                if(isGrounded())
                {
                    coyoteCounter = coyoteTime;

                }
                else
                {
                    coyoteCounter -= Time.deltaTime;
                }


            }





        }

        private void Jump()
        {
            if (coyoteCounter <= 0 && !onWall()) return;

            if (onWall())
                WallJump();
            else
            {
                if (isGrounded())
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (coyoteTime > 0)
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                }

                coyoteCounter = 0;
            }

            

        }

        private void WallJump()
        {

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