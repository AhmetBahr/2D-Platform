using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{

    private bool isGrounded;

    private int xInput;

    private bool jumpInput;

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerDate, string animBoolName) : 
        base(player, stateMachine, playerDate, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        
        isGrounded = player.CheckIfGround();
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

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        if(isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerDate.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        }

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
