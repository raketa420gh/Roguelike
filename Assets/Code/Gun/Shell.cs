using System;
using UnityEngine;

public class Shell : MonoBehaviour, IProjectile
{
    public event Action<int> OnDamageInflicted;
    
    [SerializeField] [Min(0)] private int _damage = 1;
    [SerializeField] [Min(0)] private float _speed = 10f;
    [SerializeField] [Min(0)] private float _lifeTime = 3f;
    private Vector3 _moveDirection;
    private Vector3 _targetPosition;

    private void OnEnable() => 
        Destroy(gameObject, _lifeTime);

    private void FixedUpdate()
    {
        transform.position = Vector3
            .MoveTowards(transform.position, _targetPosition, _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();

        if (health)
        {
            health.ChangeHealth(-_damage);
            Debug.Log($"Health = {health.Current}");
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target) => 
        _targetPosition = target.position;

    private void InflictDamage() => 
        OnDamageInflicted?.Invoke(_damage);
}