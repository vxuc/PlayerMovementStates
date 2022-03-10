using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("ENTER IDLE");
    }

    public override void ExitState()
    {
        Debug.Log("EXIT IDLE");
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Debug.Log("IDLING");
    }

    public override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        //check for the conditions to switch states
        if (ctx.IsMoving && ctx.IsSprinting)
            SwitchState(factory.Run());
        else if (ctx.IsMoving)
            SwitchState(factory.Walk());
    }
}
