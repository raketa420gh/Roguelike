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
    private IUnitsDetector _unitsDetector;
    private ILevelLoop _levelLoop;

    public Transform Body => _body;
    public bool InAreaOfCurrentStage => _levelLoop.CurrentStage.InArea;

    [Inject]
    public void Construct(IInputService input, IUnitsDetector unitsDetector, ILevelLoop levelLoop)
    {
        _input = input;
        _unitsDetector = unitsDetector;
        _levelLoop = levelLoop;
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
        IdleState = new IdleState(StateMachine, this, _input, _movement, _firearms, _unitsDetector);
        MoveState = new MoveState(StateMachine, this, _input, _movement);
        StateMachine.ChangeState(IdleState);
    }
}