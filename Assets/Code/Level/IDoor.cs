using System;

public interface IDoor
{
    event Action OnHeroTriggerEnter;
    void Open();
    void Close();
}