using UnityEngine;

public class MoveState : BaseState
{
    private Hero _hero;
    private IInputService _input;

    public MoveState(StateMachine stateMachine, Hero hero, IInputService input)
    {
        _hero = hero;
        _input = input;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move state enter.");
    }
    
    public override void Update()
    {
        if (_input.Axis == Vector2.zero)
            _hero.StateMachine.ChangeState(_hero.IdleState);
        
        _hero.Movement.Move(GetConvertedDirection(_input.Axis));
    }

    private Vector3 GetConvertedDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
}