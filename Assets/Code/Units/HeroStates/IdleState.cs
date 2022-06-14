using UnityEngine;

public class IdleState : BaseState
{
    private Hero _hero;
    private ICharacterMovement _movement;
    private IFirearms _firearms;
    private IInputService _input;
    private IEnemyDetector _enemyDetector;
    private Enemy _enemy;

    public IdleState(StateMachine stateMachine,
        Hero hero,
        IInputService input,
        ICharacterMovement movement,
        IFirearms firearms,
        IEnemyDetector enemyDetector)
    {
        _hero = hero;
        _input = input;
        _movement = movement;
        _firearms = firearms;
        _enemyDetector = enemyDetector;
    }

    public override void Enter()
    {
        base.Enter();
        
        _enemy = TryToFindEnemy();
        
        if (_enemy != null)
        {
            var target = _enemy.GetComponent<ITargetable>();
            _hero.Body.LookAt(_enemy.transform);
            _firearms.StartShooting(target);
        }
    }

    public override void Update()
    {
        base.Update();
        if (_input.Axis != Vector2.zero)
            _hero.StateMachine.ChangeState(_hero.MoveState);
    }

    public override void Exit()
    {
        base.Exit();
        _firearms.StopShooting();
    }

    private Enemy TryToFindEnemy() =>
        _enemyDetector.GetClosestEnemy(_hero.transform);
}