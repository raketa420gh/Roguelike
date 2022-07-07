using UnityEngine;

public interface IFactory
{
    Hero CreateHero(Vector3 position, string path = AssetPath.Hero, Transform parent = null);
    Enemy CreateEnemy(Vector3 position, string path = AssetPath.Enemy, Transform parent = null);
    IProjectile CreateShell(Vector3 position, string path = AssetPath.Shell, Transform parent = null);
    Stage CreateStageBase(Vector3 position, string path = AssetPath.StageBase, Transform parent = null);
}