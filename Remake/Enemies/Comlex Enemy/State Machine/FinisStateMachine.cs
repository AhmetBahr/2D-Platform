using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisStateMachine
{
  public AttackState currentState { get; private set; }

    public void Initialize(AttackState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(AttackState newState) 
    {
        currentState.Exit();
        currentState = newState;    
        currentState.Enter();
        
    }


}
