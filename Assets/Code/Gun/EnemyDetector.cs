using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> OnEnemyDetected;
    public event Action<Enemy> OnEnemyUnobserved;
    
    private readonly List<Enemy> _detectedEnemies = new List<Enemy>();
    private Enemy _closestEnemy;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            _detectedEnemies.Add(enemy);
            enemy.OnDead += RemoveEnemyFromDetectedList;
            OnEnemyDetected?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            RemoveEnemyFromDetectedList(enemy);
            OnEnemyUnobserved?.Invoke(enemy);
        }
    }

    public Enemy GetClosestEnemy(Transform transform)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Enemy potentialEnemy in _detectedEnemies)
        {
            Vector3 directionToTarget = potentialEnemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                _closestEnemy = potentialEnemy;
            }
        }

        return _closestEnemy;
    }

    private void RemoveEnemyFromDetectedList(Enemy enemy) => 
        _detectedEnemies.Remove(enemy);
}