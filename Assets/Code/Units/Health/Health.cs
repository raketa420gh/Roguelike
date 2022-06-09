using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnChanged;
    public event Action<float> OnPercentChanged = delegate {  }; 
    public event Action OnOver;
    private int _current;
    private int _max = 100;
    
    private void OnEnable() => 
        Restore();

    public void Setup(int maxHealth) => 
        _max = maxHealth;

    public void ChangeHealth(int amount)
    {
        _current = _current - amount;
        OnChanged?.Invoke(amount);

        float currentHealthPercent = (float) _current / (float) _max;
        OnPercentChanged?.Invoke(currentHealthPercent);

        if (_current <= 0)
            OnOver?.Invoke();
    }

    private void Restore() => 
        _current = _max;
}