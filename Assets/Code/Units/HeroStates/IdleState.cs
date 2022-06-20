using UnityEngine;

public class IdleState : BaseState
{
    private Hero _hero;
    private ICharacterMovement _movement;
    private IFirearms _firearms;
    private IInputService _input;
    private IEnemyDetector _enemyDetector;
    private Enemy _enemy;
    private Coroutine _shootingRoutine;

    private float _shootingSpeed;

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
        TryToDetectEnemy();
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

    private void TryToDetectEnemy()
    {
        _enemy = _enemyDetector.GetClosestEnemy(_hero.transform);

        if (_enemy != null)
            AttackEnemy();
    }

    private void AttackEnemy()
    {
        _enemy.OnDead += OnEnemyDead;
        var target = _enemy.GetComponent<ITargetable>();
        _hero.Body.LookAt(_enemy.transform);
        _firearms.StartShooting(target);
    }

    private void OnEnemyDead(Enemy enemy)
    {
        if (enemy == _enemy)
        {
            _firearms.StopShooting();
            TryToDetectEnemy();
        }
    }
}