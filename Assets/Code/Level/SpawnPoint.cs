using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsOccupied { get; set; }

    public Vector3 GetPosition => transform.position;
}
