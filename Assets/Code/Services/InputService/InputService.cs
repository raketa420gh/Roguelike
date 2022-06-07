using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string Jump = "Jump";

    public abstract Vector2 Axis { get; }

    protected static Vector2 GetSimpleInputAxis()
    {
        return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}