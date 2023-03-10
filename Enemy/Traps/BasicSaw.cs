using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSaw : MonoBehaviour
{
    private enum State
    {
        Moving,

    }

    private State currentState;
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementDistance;
    private bool movingLeft;

    [Header("Touch")]
    [SerializeField] private float lastTouchDamageTime;
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamage;

    [Header("Radius")]
    [SerializeField] private float groundCheckRadius;

    [Header("Transform")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform touchDamageCheck;

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Particile")]
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;


    private float[] attackDetails = new float[2];

    private int damageDirection;
    private Vector2 movement;

 

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;


    private float leftEdge;
    private float rightEdge;


    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Start()
    {
        alive = transform.Find("Radius").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
        }
    }

    private void UpdateMovingState()
    {

        CheckTouchDamage();

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            }
            else
                movingLeft = true;
        }

    }

    private void Damage(float[] attackDetails)
    {

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {

            Collider2D hit = Physics2D.OverlapCircle(touchDamageCheck.position, groundCheckRadius, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

  

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(touchDamageCheck.position, groundCheckRadius);

    }



}
