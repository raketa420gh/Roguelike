using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnChanged;
    public event Action<float> OnPercentChanged = delegate {  }; 
    public event Action OnOver;
    private int _max = 100;
    
    public int Current { get; private set; }

    public void Setup(int maxHealth)
    {
        _max = maxHealth;
        Restore();
        Debug.Log($"Current health of {name} = {Current}");
    }

    public void ChangeHealth(int amount)
    {
        Current += amount;
        OnChanged?.Invoke(amount);

        float currentHealthPercent = (float) Current / (float) _max;
        OnPercentChanged?.Invoke(currentHealthPercent);

        if (Current <= 0)
            OnOver?.Invoke();
    }

    private void Restore() => 
        Current = _max;
}