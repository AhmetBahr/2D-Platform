using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Attack")]
    [SerializeField] private float attack1Radius;

    [Header("Stun")]
    [SerializeField] private float stunDamageAmount = 1f;

    [Header("Timer")]
    [SerializeField] private float inputTimer;



    [Header("Enable")]
    [SerializeField] private bool combatEnabled;

    [Header("Position")]
    [SerializeField] private Transform attack1HitBoxPos;

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsDamageable;


    private float lastInputTime = Mathf.NegativeInfinity;

    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    private AttackDetails attackDetails;

    private Animator anim;

    private PlayerController PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();

    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {

                anim.SetTrigger("attack");
                anim.SetBool("attack",gotInput);

            }
        }
    }




    private void Damage(AttackDetails attackDetails)
    {
        if (!PC.GetDashStatus())
        {
            int direction;

            PS.DecreaseHealth(attackDetails.damageAmount);

            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            PC.Knockback(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

}
