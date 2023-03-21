using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_Movestate stateData;

    protected bool isDetecingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInminAgroRange;


    public MoveState(Entity etity,FinisStateMachine stateMachine,string animBoolName,D_Movestate stateData) : base (etity,stateMachine,animBoolName)
    {
        this.stateData = stateData; 
    }

    public override void DoChecks()
    {
        base.DoChecks();


        isDetectingLedge = entity.CheckLedge();
        isDetecingWall = entity.CheckWall();
        isPlayerInminAgroRange = entity.ChechPlayerInMinAgroRange();


    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
 
    }
}
