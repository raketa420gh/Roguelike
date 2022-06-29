using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Tester : MonoBehaviour
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
        _unitsCounter.OnAllEnemiesDead += OnAllUnitsDead;
        CreateNewStage();
        CreateEnemiesAtStage(3);
        
        var startCharacterPosition = new Vector3(0, 1, -9);
        _factory.CreateHero(startCharacterPosition);
    }

    private void CreateNewStage()
    {
        if (_levelLoop.CurrentStage != null)
        {
            var nextStagePosition = _levelLoop.CurrentStage.Position + new Vector3(0, 0, 18);
            _levelLoop.SetCurrentStage(_factory.CreateStageBase(nextStagePosition));
        }
        else
            _levelLoop.SetCurrentStage(_factory.CreateStageBase(Vector3.zero));

        _levelLoop.CurrentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
    }

    private void CreateEnemiesAtStage(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            var randomIndex = Random.Range(0, _levelLoop.CurrentStage.FreeSpawnPoints.Count);
            var enemyRandomPosition = _levelLoop.CurrentStage.FreeSpawnPoints[randomIndex].GetPosition;
            _levelLoop.CurrentStage.OccupySpawnPoint(randomIndex);

            var enemy = _unitsSpawner.SpawnEnemy(enemyRandomPosition, _enemyData);
        }
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
        CreateEnemiesAtStage(5);
    }
}