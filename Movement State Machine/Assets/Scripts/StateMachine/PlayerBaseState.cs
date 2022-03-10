public abstract class PlayerBaseState
{
    protected bool isRootState = false;
    protected PlayerStateMachine ctx;
    protected PlayerStateFactory factory;

    protected PlayerBaseState currentSuperState;
    protected PlayerBaseState currentSubState;

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if (currentSubState != null)
            currentSubState.UpdateStates();
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if (currentSubState != null)
            currentSubState.FixedUpdateStates();
    }

    protected void SwitchState(PlayerBaseState newState) 
    {
        //current state exits state
        ExitState();

        //new state enters state
        newState.EnterState();

        //if its root state, then switch the current state to the new state
        if (isRootState)
        {
            //switch current state to the new state
            ctx.currentState = newState;
        }
        else if (currentSuperState != null) //else if there is a super state, set the current super state's sub state to the new state
            currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) 
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}