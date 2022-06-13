using UnityEngine;
using Zenject;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Door _door;
    private IFactory _factory;
    
    [Inject]
    public void Construct(IFactory factory)
    {
        _factory = factory;
    }

    private void Start()
    {
        var startEnemyPosition = new Vector3(0, 1, 5);
        _factory.CreateEnemy(startEnemyPosition);
            
        var startCharacterPosition = new Vector3(0, 1, -5);
        var hero = _factory.CreateHero(startCharacterPosition);
        hero.InitializeStateMachine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _door.Open();
        }
    }
}