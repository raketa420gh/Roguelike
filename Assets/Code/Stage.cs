using UnityEngine;
using Zenject;

public class Stage : MonoBehaviour
{
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

public class EnemySpawner : IEnemySpawner
{
    private IFactory _factory;

    [Inject]
    public void Construct(IFactory factory) => _factory = factory;

    public Enemy SpawnEnemy(Vector3 position, EnemyData enemyData)
    {
        var enemy = _factory.CreateEnemy(position, enemyData.PrefabPath);
        enemy.Setup(enemyData);
        return enemy;
    }
}

public interface IEnemySpawner
{
    Enemy SpawnEnemy(Vector3 position, EnemyData enemyData);
}

public class EnemyData : ScriptableObject
{
    public string PrefabPath;
    public string Name;
    public int MaxHealthPoints;
}
