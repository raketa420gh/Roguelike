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

    public Enemy SpawnEnemyAtRandomFreeSpawnPoint(SpawnPoint[] spawnPoints, EnemyData enemyData)
    {
        var randomPositionIndex = Random.Range(0, spawnPoints.Length);
        var randomPosition = spawnPoints[randomPositionIndex].GetPosition;
        spawnPoints[randomPositionIndex].IsOccupied = true;
        
        return SpawnEnemy(randomPosition, enemyData);
    }
}