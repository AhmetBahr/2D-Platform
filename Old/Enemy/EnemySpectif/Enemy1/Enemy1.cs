using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_Idlestate idleState { get; private set; }
    public E1_Movestate moveState { get; private set; }

    public E1_PlayerDetectedState playerDetectedState { get; private set; }

    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
 
    public E1_MeleeAttack meleAttacksState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeathState deadState { get; private set; }



    [SerializeField] private D_Idlestate idleStateData;
    [SerializeField] private D_Movestate moveStateData;
    [SerializeField] private D_PlayerDected playerDetecteData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerData lookForPlayerStateData;
    [SerializeField] private D_MelleAttackState meleAttacksStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_deathState deadStateData;



    [SerializeField] private Transform meleAttackPosition;

    public override void Start()
    {
        base.Start();
        moveState = new E1_Movestate(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_Idlestate(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetecteData, this);
        chargeState = new E1_ChargeState (this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleAttacksState = new E1_MeleeAttack(this, stateMachine, "meleeAttack", meleAttackPosition, meleAttacksStateData,this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E1_DeathState(this, stateMachine, "dead", deadStateData, this);


        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();    

        Gizmos.DrawWireSphere(meleAttackPosition.position, meleAttacksStateData.attackRadius);

    }

    public override void Damage(AttacksDeatls attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }

    }
} 
