using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";

    public abstract Vector2 Axis { get; }

    protected static Vector2 GetSimpleInputAxis() => 
        new (SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
}