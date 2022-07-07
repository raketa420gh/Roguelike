using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Stage : MonoBehaviour, IStage
{
    [SerializeField] private Transform _cameraPointTranform;
    [SerializeField] List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private IDoor _door;
    
    public Vector3 Position => transform.position;
    public Vector3 CameraPosition => _cameraPointTranform.position;
    public IDoor Door => _door;
    public List<SpawnPoint> FreeSpawnPoints => _spawnPoints;
    public bool InArea { get; private set; }

    private void Awake()
    {
        _door = GetComponentInChildren<IDoor>();
        
        if (_spawnPoints.Count == 0)
            _spawnPoints.AddRange(GetComponentsInChildren<SpawnPoint>());
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

    public void OccupySpawnPoint(int index) => _spawnPoints.Remove(_spawnPoints[index]);

    public void LoadStage() => _door.Close();

    public void CompleteStage() => _door.Open();
}