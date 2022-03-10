using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { 
        isRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("ENTER JUMPING");
        Jump();
    }

    public override void ExitState()
    {
        Debug.Log("EXIT JUMPING");
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Debug.Log("JUMPING");
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        //check for the conditions to switch states
        if (ctx.IsGrounded)
        {
            SwitchState(factory.Grounded());
        }
    }

    void Jump()
    {
        //do the jump
        ctx.Rigidbody.velocity = new Vector3(ctx.Rigidbody.velocity.x, 0f, ctx.Rigidbody.velocity.z);
        ctx.Rigidbody.AddForce(ctx.Transform.up * ctx.JumpForce, ForceMode.Impulse);
    }
}
