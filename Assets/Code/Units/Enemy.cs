using UnityEngine;

[RequireComponent(typeof(Targetable))]

public class Enemy : Character
{
    public void Setup(EnemyData enemyData)
    {
        Setup();
        Health.Setup(enemyData.MaxHealthPoints);
    }
}