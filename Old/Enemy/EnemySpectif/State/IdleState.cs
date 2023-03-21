using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_Idlestate stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInminAgroRange;


    protected float idleTime;



    public IdleState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_Idlestate stateData) :
        base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInminAgroRange = entity.ChechPlayerInMinAgroRange();


    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);
        isIdleTimeOver= false;
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
