using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{

    private Enemy1 enemy;

    public E1_ChargeState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_CahargeState stateData, Enemy1 enemy) :
        base(etity, stateMachine, animBoolName , stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isDectectngLedge || isDectlectngWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);

        }
        else if (isChargeTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleAttacksState);

            }
         /*   else if (isPlayerInMinArgoRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }*/
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
