using UnityEngine;

public interface IFactory
{
    Hero CreateHero(Vector3 position, string path = AssetPath.Hero, Transform parent = null);
    Enemy CreateEnemy(Vector3 position, string path = AssetPath.Enemy, Transform parent = null);
}