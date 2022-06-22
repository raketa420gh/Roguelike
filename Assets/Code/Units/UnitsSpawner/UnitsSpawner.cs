using UnityEngine;
using Zenject;

public class UnitsSpawner : MonoBehaviour, IUnitsSpawner
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