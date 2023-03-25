using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : ComboState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        ComboState nextState = (ComboState)new GroundEntryState();
        stateMachine.SetNextState(nextState);
    }
}
