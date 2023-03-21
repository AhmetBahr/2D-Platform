using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isDectectngLedge;
    protected bool isDectlectngWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : 
        base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();


        isPlayerInMinArgoRange = entity.ChechPlayerInMinAgroRange();
        isDectectngLedge = entity.CheckLedge();
        isDectlectngWall = entity.CheckWall();
        performCloseRangeAction = entity.CheckPlayerInCloseAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver= false;
        entity.SetVelocity(stateData.chargeSpeed);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.deltaTime >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver= true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();



    }
}
