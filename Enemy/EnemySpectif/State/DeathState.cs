using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    protected D_deathState stateData;

    public DeathState(Entity etity, FinisStateMachine stateMachine, string animBoolName, D_deathState stateData) 
        : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData; 
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGo.transform.position, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGo.transform.position, stateData.deathChunkParticle.transform.rotation);

        entity.gameObject.SetActive(false); 
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
