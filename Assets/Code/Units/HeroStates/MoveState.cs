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
    
    public override void Update()
    {
        base.Update();
        if (_input.Axis == Vector2.zero)
            _hero.StateMachine.ChangeState(_hero.IdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var moveVector = GetConvertedDirection(_input.Axis);
        _hero.Movement.Move(moveVector);
        LookAt(moveVector);
    }

    private Vector3 GetConvertedDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
    
    private void LookAt(Vector3 forwardVector) => 
        _hero.Body.forward = forwardVector;
}