using UnityEngine;

public class IdleState : BaseState
{
    private Hero _hero;
    private IInputService _input;
    
    public IdleState(StateMachine stateMachine, Hero hero, IInputService input)
    {
        _hero = hero;
        _input = input;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle state enter.");
    }

    public override void Update()
    {
        if (_input.Axis != Vector2.zero)
            _hero.StateMachine.ChangeState(_hero.MoveState);
    }
}