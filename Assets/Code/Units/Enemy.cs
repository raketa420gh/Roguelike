using System;
using UnityEngine;

[RequireComponent(typeof(Targetable))]
public class Enemy : Character
{
    public event Action<Enemy> OnDead;
    private Health _health;

    private void OnEnable() =>
        _health.OnOver += OnHealthOver;

    private void OnDisable() =>
        _health.OnOver -= OnHealthOver;

    private void Awake() =>
        _health = GetComponent<Health>();

    public void Setup(EnemyData enemyData)
    {
        _health.Setup(enemyData.MaxHealthPoints);
    }

    private void Die()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnHealthOver() => 
        Die();
}