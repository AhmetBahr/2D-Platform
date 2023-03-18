using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Spikes : MonoBehaviour
{
    private enum State
    {
        Moving
    }

    private State currentState;
    [Header("Touch")]
    [SerializeField] private float lastTouchDamageTime;
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamage;

    [Header("Size")]
    [SerializeField] private float touchDamageWidth;
    [SerializeField] private float touchDamageHeight;


    [Header("Transfrom")]
    [SerializeField] private Transform touchDamageCheck;

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsPlayer;

    private float[] attackDetails = new float[2];

    private int damageDirection;

    private Vector2 movement;
    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;

    private GameObject alive;
    private Rigidbody2D aliveRb;

    private void Start()
    {
        alive = transform.Find("Boudy").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();

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

    }

    private void Damage(float[] attackDetails)
    {
        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }


    }

    public void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));




            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);



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


        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);

    }
}
