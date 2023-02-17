using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace playerCont
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;


        private Rigidbody2D body;
        private Animator anim;
        //private bool grounded;
        private BoxCollider2D boxCollider;
        private float wallJumpCooldown;

        private void Awake()
        { 
            //Refernas
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();   
            boxCollider= GetComponent<BoxCollider2D>();
        }
        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2( horizontalInput * speed, body.velocity.y);

            //Karakteri sag sola dondurmek icin
            if (horizontalInput > 0.01f)
                transform.localScale = new Vector3(8,8,8);
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-8, 8, 8);


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                Jump();

            //Animator parametreleri
            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", isGrounded());

            if(wallJumpCooldown <= 0.2f )
            {


                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                if (onWall() && !isGrounded())
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
                else
                    body.gravityScale = 7;

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    Jump();
            }
            else
                wallJumpCooldown += Time.deltaTime;
        
        }

        private void Jump()
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetTrigger("Jump");
            }
            else if (onWall() && !isGrounded())
            {

            }
            
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
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,new Vector2(transform.localScale.x,0), 0.1f, wallLayer);


            return raycastHit.collider != null;
        }
    }

}