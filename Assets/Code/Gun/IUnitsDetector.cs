using System;
using UnityEngine;

public interface IUnitsDetector
{
    event Action<Enemy> OnEnemyDetected;
    event Action<Enemy> OnEnemyUnobserved;
    Enemy GetClosestEnemy(Transform transform);
}