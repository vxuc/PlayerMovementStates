using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    bool jump = false;
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("ENTER GROUNDED");
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        if (!ctx.IsMoving && !ctx.IsSprinting)
            SetSubState(factory.Idle());
        else if (ctx.IsMoving && !ctx.IsSprinting)
            SetSubState(factory.Walk());
        else
            SetSubState(factory.Run());
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        //if a jump has been initiated, jump in fixed update
        if (jump)
        {
            SwitchState(factory.Jump());
            jump = false;
        }
    }

    public override void CheckSwitchStates()
    {
        //check for the conditions to switch states
        if (Input.GetKey(ctx.jumpKey))
        {
            //initiate a jump
            jump = true;
        }
    }
}
