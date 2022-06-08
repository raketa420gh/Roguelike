using UnityEngine;
using Zenject;

public class Tester : MonoBehaviour
{
    private IFactory _factory;
    
    [Inject]
    public void Construct(IFactory factory)
    {
        _factory = factory;
    }

    private void Start()
    {
        var startCharacterPosition = new Vector3(0, 1, -5);
        _factory.CreateHero(startCharacterPosition);

        var startEnemyPosition = new Vector3(0, 1, 5);
        _factory.CreateEnemy(startEnemyPosition);
    }
}