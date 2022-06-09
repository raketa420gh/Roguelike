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
        var enemy = _hero.EnemyDetector.GetClosestEnemy(_hero.transform);

        if (enemy != null)
        {
            var target = enemy.GetComponent<ITargetable>();
            _hero.Firearms.StartShooting(target);
        }
    }

    public override void Update()
    {
        base.Update();
        if (_input.Axis != Vector2.zero)
            _hero.StateMachine.ChangeState(_hero.MoveState);
    }
}