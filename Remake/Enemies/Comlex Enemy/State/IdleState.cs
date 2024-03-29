using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class IdleState : AttackState
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInminAgroRange;


    protected float idleTime;



    public IdleState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_IdleState stateData) :
        base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInminAgroRange = entity.CheckPlayerInMinAgroRange();


    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();

    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime * idleTime)
        {
            isIdleTimeOver = true;

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;

    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
