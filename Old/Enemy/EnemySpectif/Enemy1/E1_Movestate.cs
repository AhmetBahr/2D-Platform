using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Movestate : MoveState
{

    private Enemy1 enemy;


    public E1_Movestate(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_Movestate stateData, Enemy1 enemy) :
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
        else if (isDetecingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
