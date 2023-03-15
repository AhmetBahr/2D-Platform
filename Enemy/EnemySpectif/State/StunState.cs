using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;

    protected bool isStýnTimeOver;
    protected bool isGrounded;
    protected bool isMoveStopped;
    protected bool performanceCloseRangerAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Entity etity, FinisStateMachine stateMachine, string animBoolName,D_StunState stateData)
        : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
        performanceCloseRangerAction = entity.CheckPlayerInCloseAction();
        isPlayerInMinAgroRange = entity.ChechPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStýnTimeOver= false;
        isMoveStopped=false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnocbackAngle,entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.deltaTime >= startTime+ stateData.stunTime)
        {
            isStýnTimeOver = true;  
        }

        if(isGrounded && Time.time >= startTime + stateData.stunKnocbackTime && !isMoveStopped)
        {
            isMoveStopped= true;
            entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
