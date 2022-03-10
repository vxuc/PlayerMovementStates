using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("ENTER RUNNING");
        ctx.MovementSpeed = ctx.SprintSpeed;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT RUNNING");
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Debug.Log("RUNNING");
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        //check for the conditions to switch states
        if (!ctx.IsMoving)
            SwitchState(factory.Idle());
        else if (ctx.IsMoving && !ctx.IsSprinting)
            SwitchState(factory.Walk());
    }
}
