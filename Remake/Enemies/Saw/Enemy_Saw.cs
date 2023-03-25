using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Saw : MonoBehaviour
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

    private AttackDetails attackDetails;


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

    private void Damage(AttackDetails attackDetails)
    {


        if (attackDetails.position.x > alive.transform.position.x)
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
                attackDetails.damageAmount = touchDamage;
                attackDetails.position.x = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }



    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(touchDamageCheck.position, groundCheckRadius);

    }
}
