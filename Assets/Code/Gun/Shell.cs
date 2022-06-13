using System;
using UnityEngine;

public class Shell : MonoBehaviour, IProjectile
{
    public event Action<int> OnDamageInflicted;
    
    [SerializeField] [Min(0)] private int _damage = 10;
    [SerializeField] [Min(0)] private float _speed = 50f;
    [SerializeField] [Min(0)] private float _lifeTime = 2f;
    private Vector3 _moveDirection;
    private Vector3 _targetPosition;
    private IProjectile _projectileImplementation;

    private void OnEnable() => 
        Destroy(gameObject, _lifeTime);

    private void FixedUpdate()
    {
        transform.position = Vector3
            .MoveTowards(transform.position, _targetPosition, _speed * Time.fixedDeltaTime);
    }

    public void SetTarget(Transform target) => 
        _targetPosition = target.position;

    private void InflictDamage() => 
        OnDamageInflicted?.Invoke(_damage);
}