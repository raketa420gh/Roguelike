using System;

public class Character : Unit
{
    public event Action<Character> OnDead;
    
    private Health _health;

    protected Health Health => _health;

    private void Awake() =>
        _health = GetComponent<Health>();

    protected void Setup()
    {
        _health.OnOver += OnHealthOver;
    }

    private void Die()
    {
        _health.OnOver -= OnHealthOver;
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnHealthOver() =>
        Die();
}