using System.Threading;
using UnityEngine;

public class IdleState : BaseState
{
    private Hero _hero;
    private ICharacterMovement _movement;
    private ICharacterAnimation _animation;
    private IFirearms _firearms;
    private IInputService _input;
    private IUnitsDetector _unitsDetector;
    private Enemy _enemy;
    private float _shootingSpeed;
    private CancellationTokenSource _tokenSource;
    
    public IdleState(StateMachine stateMachine,
        Hero hero,
        IInputService input,
        ICharacterMovement movement,
        ICharacterAnimation animation,
        IFirearms firearms,
        IUnitsDetector unitsDetector)
    {
        _hero = hero;
        _input = input;
        _movement = movement;
        _animation = animation;
        _firearms = firearms;
        _unitsDetector = unitsDetector;
    }

    public override void Enter()
    {
        base.Enter();
        _animation.PlayIdleAnimation();
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
        if (_tokenSource != null)
            _firearms.StopShooting(_tokenSource);
    }

    private void TryToDetectEnemy()
    {
        _enemy = _unitsDetector.GetClosestEnemy(_hero.transform);

        if (_enemy != null && _hero.InAreaOfCurrentStage)
            AttackEnemy();
    }

    private void AttackEnemy()
    {
        _animation.PlayAttackAnimation();
        _enemy.OnDead += OnEnemyDead;
        var target = _enemy.GetComponent<ITargetable>();
        _hero.Body.LookAt(_enemy.transform);
        _tokenSource = new CancellationTokenSource();
        _firearms.StartShooting(target, _tokenSource.Token);
    }

    private void OnEnemyDead(Character character)
    {
        if (character == _enemy)
        {
            _firearms.StopShooting(_tokenSource);
            TryToDetectEnemy();
        }
    }
}