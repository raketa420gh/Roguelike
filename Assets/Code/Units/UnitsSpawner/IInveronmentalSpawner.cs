using UnityEngine;

public interface IInveronmentalSpawner
{
    void InitializeStagePathList();
    Stage SpawnRandomStage(Vector3 position);
}