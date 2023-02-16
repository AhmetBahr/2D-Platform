using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace playerCont
{
   public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private float speed;
        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
                body.velocity = new Vector2(body.velocity.x, speed);    


        }
    }

}