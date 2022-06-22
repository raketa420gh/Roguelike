using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnPoints;
    private IDoor _door;

    public IDoor Door => _door;

    private void Awake()
    {
        _door = GetComponentInChildren<IDoor>();
    }

    public void LoadStage()
    {
        _door.Close();
        
    }
}