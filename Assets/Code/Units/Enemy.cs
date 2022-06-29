using UnityEngine;

[RequireComponent(typeof(Targetable))]

public class Enemy : Character
{
    public void Setup(EnemyData enemyData)
    {
        _health.Setup(enemyData.MaxHealthPoints);
    }
}