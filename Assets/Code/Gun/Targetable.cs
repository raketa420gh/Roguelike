using UnityEngine;

public class Targetable : MonoBehaviour, ITargetable
{
    public Transform GetTransform() => 
        gameObject.transform;

    public Vector3 GetDirectionRelativeTo(Transform aiming) => 
        transform.position - aiming.transform.position;
}