using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Stage : MonoBehaviour, IStage
{
    [SerializeField] private Transform _cameraPointTranform;
    [SerializeField] List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private IDoor _door;

    public List<SpawnPoint> FreeSpawnPoints => _spawnPoints;
    public bool InArea { get; private set; }
    public IDoor Door => _door;
    public Vector3 CameraPosition => _cameraPointTranform.position;

    private void Awake()
    {
        _door = GetComponentInChildren<IDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var hero = other.GetComponent<Hero>();

        if (hero)
            InArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var hero = other.GetComponent<Hero>();

        if (hero)
            InArea = false;
    }

    public void OccupeSpawnPoint(int index) => _spawnPoints.Remove(_spawnPoints[index]);

    public void LoadStage() => _door.Close();

    public void CompleteStage() => _door.Open();
}