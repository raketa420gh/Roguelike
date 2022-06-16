using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]

public class Hero : Character
{
    public StateMachine StateMachine;
    public IdleState IdleState;
    public MoveState MoveState;

    [SerializeField] private Transform _body;
    private ICharacterMovement _movement;
    private IFirearms _firearms;
    private IInputService _input;
    private IEnemyDetector _enemyDetector;
    
    public Transform Body => _body;

    [Inject]
    public void Construct(IInputService input, IEnemyDetector enemyDetector)
    {
        _input = input;
        _enemyDetector = enemyDetector;
    }

    private void Awake()
    {
        _movement = GetComponent<ICharacterMovement>();
        _firearms = GetComponentInChildren<IFirearms>();
    }

    private void Start() => InitializeStateMachine();

    private void Update() => StateMachine.CurrentState.Update();

    private void FixedUpdate() => StateMachine.CurrentState.FixedUpdate();

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        IdleState = new IdleState(StateMachine, this, _input, _movement, _firearms, _enemyDetector);
        MoveState = new MoveState(StateMachine, this, _input, _movement);
        StateMachine.ChangeState(IdleState);
    }
}