using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDected stateDate;

    protected bool isPlayerMinAgroRange;
    protected bool isPlayerMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performanCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_PlayerDected stateData) : 
        base(etity, stateMachine, animBoolName)
    {
        this.stateDate = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinAgroRange = entity.ChechPlayerInMinAgroRange();
        isPlayerMaxAgroRange = entity.ChechPlayerInMaxAgroRange();
        isDetectingLedge = entity.CheckLedge();
        performanCloseRangeAction = entity.CheckPlayerInCloseAction();

    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction= false;
        entity.SetVelocity(0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateDate.longRangeActionTime)
        {
            performLongRangeAction= true; 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
