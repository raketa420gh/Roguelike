using UnityEngine;

public class StateMachine
{
    public BaseState CurrentState { get; private set; }

    public void ChangeState(BaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;

        if (CurrentState == null)
            return;

        CurrentState.StateMachine = this;
        CurrentState.Enter();
        
        Debug.Log($"{newState} state enter");
    }
}