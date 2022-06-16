using System;
using UnityEngine;
using Zenject;

public class EnemyCounter : MonoBehaviour, IEnemyCounter
{
    public event Action OnAllEnemiesDead;
    
    private IEnemyDetector _enemyDetector;
    private int _enemyCount = 0;

    [Inject]
    public void Construct(IEnemyDetector enemyDetector) =>
        _enemyDetector = enemyDetector;

    private void OnEnable() => _enemyDetector.OnEnemyDetected += OnEnemyDetected;

    private void OnDisable() => _enemyDetector.OnEnemyDetected -= OnEnemyDetected;

    private void OnEnemyDetected(Enemy enemy)
    {
        _enemyCount++;
        
        enemy.OnDead += OnEnemyDead;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        _enemyCount--;

        if (_enemyCount <= 0)
        {
            _enemyCount = 0;
            OnAllEnemiesDead?.Invoke();
        }
    }
}