using UnityEngine;

public interface IEnemySpawner
{
    Enemy SpawnEnemy(Vector3 position, EnemyData enemyData);
}