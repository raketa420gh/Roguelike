using UnityEngine;

public interface IUnitsSpawner
{
    Enemy SpawnEnemy(Vector3 position, EnemyData enemyData);
}