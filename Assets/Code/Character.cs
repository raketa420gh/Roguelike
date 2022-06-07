public class Character : Unit
{
    public StateMachine MovementStateMachine;
    public IdleState IdleState;
    
    private void InitializeStateMachine()
    {
        MovementStateMachine = new StateMachine();
        IdleState = new IdleState(this, MovementStateMachine);
        MovementStateMachine.ChangeState(IdleState);
    }

    private void Update()
    {
        MovementStateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        MovementStateMachine.CurrentState.FixedUpdate();
    }
}