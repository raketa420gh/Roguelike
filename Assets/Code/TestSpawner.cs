using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Door _door;
    private IFactory _factory;
    private IEnemyCounter _enemyCounter;
    
    [Inject]
    private void Construct(IFactory factory, IEnemyCounter enemyCounter)
    {
        _factory = factory;
        _enemyCounter = enemyCounter;
    }

    private void Start()
    {
        _enemyCounter.OnAllEnemiesDead += OnAllEnemiesDead;
        
        var startEnemyPosition1 = new Vector3(0, 1, 5);
        _factory.CreateEnemy(startEnemyPosition1);

        var startEnemyPosition2 = new Vector3(-3, 1, 5);
        _factory.CreateEnemy(startEnemyPosition2);
        
        var startEnemyPosition3 = new Vector3(3, 1, 5);
        _factory.CreateEnemy(startEnemyPosition3);
        
        var startCharacterPosition = new Vector3(0, 1, -5);
        CreateHeroWithDelayAsync(0.1f, startCharacterPosition);
    }

    private void OnAllEnemiesDead()
    {
        _door.Open();
    }

    private async Task CreateHeroWithDelayAsync(float seconds, Vector3 position)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        _factory.CreateHero(position);
    }
}