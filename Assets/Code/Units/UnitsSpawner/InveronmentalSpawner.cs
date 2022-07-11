using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InveronmentalSpawner: MonoBehaviour, IInveronmentalSpawner
{
    private IFactory _factory;
    private List<string> _stagePathList = new List<string>();

    [Inject]
    public void Construct(IFactory factory) => _factory = factory;

    public void InitializeStagePathList()
    {
        _stagePathList.Add(AssetPath.StageBase);
        _stagePathList.Add(AssetPath.StageBase2);
        _stagePathList.Add(AssetPath.StageBase3);
        _stagePathList.Add(AssetPath.StageBase4);
    }

    public Stage SpawnRandomStage(Vector3 position)
    {
        var randomIndex = Random.Range(0, _stagePathList.Count);
        return _factory.CreateStageBase(position, _stagePathList[randomIndex]);
    }
}