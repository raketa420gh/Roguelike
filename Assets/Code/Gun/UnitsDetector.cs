using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class UnitsDetector : MonoBehaviour, IUnitsDetector
{
    public event Action<Enemy> OnEnemyDetected;
    public event Action<Enemy> OnEnemyUnobserved;

    private readonly List<Enemy> _detectedEnemies = new();
    private Enemy _closestEnemy;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();

        if (enemy)
        {
            _detectedEnemies.Add(enemy);
            enemy.OnDead += RemoveEnemyFromDetectedList;
            Debug.Log($"On enemy detected {enemy.name}");
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