using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeathSatate : DeadState
{
    private Enemy2 enemy;

    public E2_DeathSatate(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy2 enemy) 
        : base(etity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
