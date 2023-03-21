using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    protected Enemy1 enemy;

    public E1_PlayerDetectedState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_PlayerDected stateData, Enemy1 enemy) : 
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

        if (performanCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleAttacksState);
        }
        else if (performLongRangeAction)
        {  
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState); 
        }



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
