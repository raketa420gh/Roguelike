using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPointsParentObject;
    private SpawnPoint[] _spawnPoints;
    private IDoor _door;

    public IDoor Door => _door;
    public SpawnPoint[] SpawnPoints => _spawnPoints;

    private void Awake()
    {
        _door = GetComponentInChildren<IDoor>();
        _spawnPoints = _spawnPointsParentObject.GetComponentsInChildren<SpawnPoint>();
    }

    public void LoadStage()
    {
        _door.Close();
    }
}