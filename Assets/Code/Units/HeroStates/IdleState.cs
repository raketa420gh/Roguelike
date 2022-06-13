using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class IdleState : BaseState
{
    private Hero _hero;
    private ICharacterMovement _movement;
    private IFirearms _firearms;
    private IInputService _input;
    private IEnemyDetector _enemyDetector;
    
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

        var enemy = TryToFindEnemyAfterDelay(0.05f);
        
        if (enemy.Result != null)
        {
            var target = enemy.Result.GetComponent<ITargetable>();
            _hero.LookAt(target.GetTransform());
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

    private async Task<Enemy> TryToFindEnemyAfterDelay(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        return TryToFindEnemy();
    }
}