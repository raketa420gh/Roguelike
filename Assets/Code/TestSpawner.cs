using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Door _door;
    private IFactory _factory;
    private IEnemyCounter _enemyCounter;
    private Camera _camera = Camera.main;
    private Stage _currentStage;
    
    [Inject]
    private void Construct(IFactory factory, IEnemyCounter enemyCounter)
    {
        _factory = factory;
        _enemyCounter = enemyCounter;
    }

    private void Start()
    {
        _currentStage  = _factory.CreateStageBase(Vector3.zero);
        _currentStage.Door.OnHeroTriggerEnter += OnHeroEnterDoor;
        
        _enemyCounter.OnAllEnemiesDead += OnAllEnemiesDead;

        var startEnemyPosition2 = new Vector3(-3, 1, 5);
        _factory.CreateEnemy(startEnemyPosition2);
        
        var startEnemyPosition3 = new Vector3(3, 1, 5);
        _factory.CreateEnemy(startEnemyPosition3);
        
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

    private void OnAllEnemiesDead()
    {
        _currentStage.Door.Open();
        _currentStage = _factory.CreateStageBase(new Vector3(0, 0, 18));
    }
}