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
    private Stage _currentStage;
    
    [Inject]
    private void Construct(IFactory factory, IUnitsSpawner unitsSpawner, IUnitsCounter unitsCounter)
    {
        _factory = factory;
        _unitsSpawner = unitsSpawner;
        _unitsCounter = unitsCounter;
    }

    private void Start()
    {
        _currentStage  = _factory.CreateStageBase(Vector3.zero);
        _currentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
        
        _unitsCounter.OnAllEnemiesDead += OnAllUnitsDead;

        var randomPositionIndex = Random.Range(0, _currentStage.SpawnPoints.Length);
        var randomPosition = _currentStage.SpawnPoints[randomPositionIndex].transform.position;
        
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
        _currentStage.Door.Open();
        _currentStage = _factory.CreateStageBase(new Vector3(0, 0, 18));
        _currentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
        
        var randomPositionIndex = Random.Range(0, _currentStage.SpawnPoints.Length);
        var randomPosition = _currentStage.SpawnPoints[randomPositionIndex].transform.position;
        
        var enemy1 = _unitsSpawner.SpawnEnemy(randomPosition, _enemyData);
        enemy1.Setup(_enemyData);
    }
}