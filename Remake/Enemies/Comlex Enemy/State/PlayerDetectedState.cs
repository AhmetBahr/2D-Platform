using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerDetectedState : AttackState
{
    protected D_PlayerDetectedState stateDate;

    protected bool isPlayerMinAgroRange;
    protected bool isPlayerMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performanCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) :
        base(etity, stateMachine, animBoolName)
    {
        this.stateDate = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingLedge = entity.CheckLedge();
        performanCloseRangeAction = entity.CheckPlayerInCloseRangeAction();

    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        entity.SetVelocity(0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateDate.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
