using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ChargeState : AttackState
{
    protected D_CahargeState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isDectectngLedge;
    protected bool isDectlectngWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_CahargeState stateData) :
        base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();


        isPlayerInMinArgoRange = entity.CheckPlayerInMinAgroRange();
        isDectectngLedge = entity.CheckLedge();
        isDectlectngWall = entity.CheckWall();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        entity.SetVelocity(stateData.chargeSpeed);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();



    }
}
