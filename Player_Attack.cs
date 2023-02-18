using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace playerCont
{
    public class Player_Attack : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        private Animator anim;
        private PlayerMovement playerMovement;
        private float cooldownTimer = Mathf.Infinity;

        private void Awake()
        {
            anim= GetComponent<Animator>();
            playerMovement= GetComponent<PlayerMovement>();
        
        
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
                Attack();

            cooldownTimer += Time.deltaTime;
        }

        private void Attack()
        {
            anim.SetTrigger("Attack");
            cooldownTimer = 0; 
        }


    }
}