using UnityEngine;

public class MoveState : BaseState
{
    private Hero _hero;
    private IInputService _input;
    private ICharacterMovement _movement;
    private ICharacterAnimation _animation;
    private float _moveSpeed = 3f;

    public MoveState(StateMachine stateMachine, 
        Hero hero, 
        IInputService input, 
        ICharacterMovement movement,
        ICharacterAnimation animation)
    {
        _hero = hero;
        _input = input;
        _movement = movement;
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();
        _animation.PlayRunAnimation();
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
        _movement.Move(moveVector.normalized * _moveSpeed);
        LookAt(moveVector);
    }

    private Vector3 GetConvertedDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
    
    private void LookAt(Vector3 forwardVector) => 
        _hero.Body.forward = forwardVector;
}