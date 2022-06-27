using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Stage : MonoBehaviour, IStage
{
    [SerializeField] private Transform _spawnPointsParentObject;
    [SerializeField] private Transform _cameraPointTranform;
    private SpawnPoint[] _spawnPoints;
    private IDoor _door;

    public bool InArea { get; private set; }
    public IDoor Door => _door;
    public SpawnPoint[] SpawnPoints => _spawnPoints;
    public Vector3 CameraPosition => _cameraPointTranform.position;

    private void Awake()
    {
        _door = GetComponentInChildren<IDoor>();
        _spawnPoints = _spawnPointsParentObject.GetComponentsInChildren<SpawnPoint>();
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

    public void LoadStage() => _door.Close();

    public void CompleteStage() => _door.Open();
}