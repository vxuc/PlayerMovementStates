using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("ENTER WALKING");
        ctx.MovementSpeed = ctx.WalkSpeed;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT WALKING");
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Debug.Log("WALKING");
    }

    public override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        //check for the conditions to switch states
        if (!ctx.IsMoving)
            SwitchState(factory.Idle());
        else if (ctx.IsMoving && ctx.IsSprinting)
            SwitchState(factory.Run());
    }
}
