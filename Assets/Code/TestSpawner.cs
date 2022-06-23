using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private IFactory _factory;
    private IUnitsSpawner _unitsSpawner;
    private IUnitsCounter _unitsCounter;
    private ILevelLoop _levelLoop;
    
    [Inject]
    private void Construct(IFactory factory, 
        IUnitsSpawner unitsSpawner, 
        IUnitsCounter unitsCounter, 
        ILevelLoop levelLoop)
    {
        _factory = factory;
        _unitsSpawner = unitsSpawner;
        _unitsCounter = unitsCounter;
        _levelLoop = levelLoop;
    }

    private void Start()
    {
        _levelLoop.SetCurrentStage(_factory.CreateStageBase(Vector3.zero));
        _levelLoop.CurrentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
        
        _unitsCounter.OnAllEnemiesDead += OnAllUnitsDead;

        var randomPositionIndex = Random.Range(0, _levelLoop.CurrentStage.SpawnPoints.Length);
        var randomPosition = _levelLoop.CurrentStage.SpawnPoints[randomPositionIndex].transform.position;
        
        var enemy1 = _unitsSpawner.SpawnEnemy(randomPosition, _enemyData);
        enemy1.Setup(_enemyData);
        
        var startCharacterPosition = new Vector3(0, 1, -9);
        CreateHeroWithDelayAsync(0.1f, startCharacterPosition);
    }

    private async Task CreateHeroWithDelayAsync(float seconds, Vector3 position)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        _factory.CreateHero(position);
    }

    private void OnHeroEnterDoor()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(Camera.main.transform.DOMove(new Vector3(0, 15, 11), 1f));
    }

    private void OnAllUnitsDead()
    {
        _levelLoop.CurrentStage.CompleteStage();
        _levelLoop.SetCurrentStage(_factory.CreateStageBase(new Vector3(0, 0, 18)));
        _levelLoop.CurrentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
        
        var randomPositionIndex = Random.Range(0, _levelLoop.CurrentStage.SpawnPoints.Length);
        var randomPosition = _levelLoop.CurrentStage.SpawnPoints[randomPositionIndex].transform.position;
        
        var enemy1 = _unitsSpawner.SpawnEnemy(randomPosition, _enemyData);
        enemy1.Setup(_enemyData);
    }
}