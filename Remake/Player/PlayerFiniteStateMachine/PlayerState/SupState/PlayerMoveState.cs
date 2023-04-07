using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerDate, string animBoolName) : 
        base(player, stateMachine, playerDate, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();
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

        player.CheckIfShouldFlip(Xinput);

        player.SetVelocityX(playerDate.movementVelocity * Xinput);

        if(Xinput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
