using System;
using UnityEngine;
using Zenject;

public class UnitsCounter : MonoBehaviour, IUnitsCounter
{
    public event Action OnAllEnemiesDead;
    
    private IUnitsDetector _unitsDetector;
    private int _enemyCount = 0;

    [Inject]
    public void Construct(IUnitsDetector unitsDetector) =>
        _unitsDetector = unitsDetector;

    private void OnEnable() => _unitsDetector.OnEnemyDetected += OnUnitsDetected;

    private void OnDisable() => _unitsDetector.OnEnemyDetected -= OnUnitsDetected;

    private void OnUnitsDetected(Enemy enemy)
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