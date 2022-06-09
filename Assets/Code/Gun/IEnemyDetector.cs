using System;
using UnityEngine;

public interface IEnemyDetector
{
    event Action<Enemy> OnEnemyDetected;
    event Action<Enemy> OnEnemyUnobserved;
    Enemy GetClosestEnemy(Transform transform);
}