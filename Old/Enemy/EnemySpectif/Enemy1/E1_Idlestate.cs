using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Idlestate : IdleState
{
    private Enemy1 enemy;
    public E1_Idlestate(Entity etity, FinisStateMachine stateMachine, string animBoolName,D_Idlestate stateData,  Enemy1 enemy) : 
        base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInminAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
