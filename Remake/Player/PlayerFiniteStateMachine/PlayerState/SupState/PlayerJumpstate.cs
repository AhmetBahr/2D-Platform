using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpstate : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpstate(Player player, PlayerStateMachine stateMachine, PlayerData playerDate, string animBoolName) : 
        base(player, stateMachine, playerDate, animBoolName)
    {
        amountOfJumpsLeft = playerDate.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerDate.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
    }

    public bool CanJump()
    {
        if(amountOfJumpsLeft > 0){
            return true;
        }
        else 
        {
            return false; 
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerDate.amountOfJumps;

    public void DexreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

}
