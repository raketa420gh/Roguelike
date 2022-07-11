using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Tester : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private CameraController _camera;
    private IFactory _factory;
    private IInveronmentalSpawner _inveronmentalSpawner;
    private IUnitsSpawner _unitsSpawner;
    private IUnitsCounter _unitsCounter;
    private ILevelLoop _levelLoop;
    
    [Inject]
    private void Construct(IFactory factory, 
        IInveronmentalSpawner inveronmentalSpawner,
        IUnitsSpawner unitsSpawner, 
        IUnitsCounter unitsCounter, 
        ILevelLoop levelLoop)
    {
        _factory = factory;
        _inveronmentalSpawner = inveronmentalSpawner;
        _unitsSpawner = unitsSpawner;
        _unitsCounter = unitsCounter;
        _levelLoop = levelLoop;
    }

    private void Start()
    {
        _unitsCounter.OnAllEnemiesDead += OnAllUnitsDead;
        _inveronmentalSpawner.InitializeStagePathList();
        
        CreateNewStage();
        CreateEnemiesAtStage(3);
        var hero = CreateHero();
        hero.Setup(0.25f);
        SetCameraFollowTo(hero.transform);
    }

    private void SetCameraFollowTo(Transform transform)
    {
        _camera.LookAt(transform);
        _camera.Follow(transform);
    }

    private Hero CreateHero()
    {
        var startCharacterPosition = new Vector3(0, 0.5f, -9);
        return _factory.CreateHero(startCharacterPosition);
    }

    private void CreateNewStage()
    {
        if (_levelLoop.CurrentStage != null)
        {
            var nextStagePosition = _levelLoop.CurrentStage.Position + new Vector3(0, 0, 18);
            _levelLoop.SetCurrentStage(_inveronmentalSpawner.SpawnRandomStage(nextStagePosition));
        }
        else
            _levelLoop.SetCurrentStage(_inveronmentalSpawner.SpawnRandomStage(Vector3.zero));
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

    private void OnAllUnitsDead()
    {
        _levelLoop.CurrentStage.CompleteStage();
        CreateNewStage();
        CreateEnemiesAtStage(3);
    }
}