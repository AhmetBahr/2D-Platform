using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Attack_Melle_State : MelleAttackState
{
    private Enemy1 enemy;

    public E1_Attack_Melle_State(Entity etity, FinisStateMachine stateMachine, string animBoolName, Transform attackPosition, D_Attack_Melee_State stateData, Enemy1 enemy)
        : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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

        if (isAnimationFinished)
        {

            if (isAnimationFinished)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
