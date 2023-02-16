using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace playerCont
{
   public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private float speed;
        private Rigidbody2D body;
        private Animator anim;
        private bool grounded;

        private void Awake()
        { 
            //Refernas
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();    
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


            if (Input.GetKeyDown(KeyCode.Space) && grounded)
                Jump();

            //Animator parametreleri
            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", grounded);
        }

        private void Jump()
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            anim.SetTrigger("Jump");
            grounded = false;
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "grounded")
                grounded = true;
        }

    }

}