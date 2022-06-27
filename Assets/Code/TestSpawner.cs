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
        _factory.CreateHero(startCharacterPosition);
    }

    private void CreateNewStage()
    {
        var nextStagePosition = _levelLoop.CurrentStage.transform.position + new Vector3(0, 0, 18);
        _levelLoop.SetCurrentStage(_factory.CreateStageBase(nextStagePosition));
        _levelLoop.CurrentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
    }

    private void CreateEnemiesAtStage()
    {
        var randomIndex = Random.Range(0, _levelLoop.CurrentStage.SpawnPoints.Length);
        var enemyRandomPosition = _levelLoop.CurrentStage.SpawnPoints[randomIndex].transform.position;

        var enemy1 = _unitsSpawner.SpawnEnemy(enemyRandomPosition, _enemyData);
        enemy1.Setup(_enemyData);
    }

    private void OnHeroEnterDoor()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(Camera.main.transform.DOMove(_levelLoop.CurrentStage.CameraPosition, 1f));
    }

    private void OnAllUnitsDead()
    {
        _levelLoop.CurrentStage.CompleteStage();
        CreateNewStage();
        CreateEnemiesAtStage();
    }
}