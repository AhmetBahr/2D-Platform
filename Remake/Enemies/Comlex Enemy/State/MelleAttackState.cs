using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MelleAttackState : AttackState
{

    protected D_Attack_Melee_State stateData;

    private AttackDetails attacksDetails;

    public MelleAttackState(Entity etity, FinisStateMachine stateMachine, string animBoolName, Transform attackPosition, D_Attack_Melee_State stateData)
        : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        attacksDetails.damageAmount = stateData.attackDamage;
        attacksDetails.position = entity.aliveGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attacksDetails); 
        }

    }
}
