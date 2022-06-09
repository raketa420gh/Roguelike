using UnityEngine;

public interface ITargetable
{
    Transform GetTransform();
    Vector3 GetDirectionRelativeTo(Transform aiming);
}