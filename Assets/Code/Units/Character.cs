using System;

public class Character : Unit
{
    public event Action<Character> OnDead;
    protected Health _health;

    private void Awake() =>
        _health = GetComponent<Health>();

    private void OnEnable() =>
        _health.OnOver += OnHealthOver;

    private void OnDisable() =>
        _health.OnOver -= OnHealthOver;

    private void Die()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnHealthOver() =>
        Die();
}