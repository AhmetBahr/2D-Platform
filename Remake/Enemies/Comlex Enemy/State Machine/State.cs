using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FinisStateMachine stateMachine;
    protected Entity entity;


    protected string animBoolName;

    public float startTime { get; protected set; }

    public State(Entity etity,FinisStateMachine stateMachine, string animBoolName)
    {
        this.entity = etity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime= Time.time;
        entity.anim.SetBool(animBoolName,true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName,false);
    }

    public virtual void LogicUpdate()
    {


    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }

}
