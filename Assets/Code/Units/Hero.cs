using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]

public class Hero : Character
{
    public StateMachine StateMachine;
    public IdleState IdleState;
    public MoveState MoveState;

    [SerializeField] private Transform _body;
    private CharacterMovement _movement;
    private IInputService _input;
    
    public CharacterMovement Movement => _movement;
    public Transform Body => _body;

    [Inject]
    public void Construct(IInputService input) => 
        _input = input;

    private void Awake() => 
        _movement = GetComponent<CharacterMovement>();

    private void Start()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        IdleState = new IdleState(StateMachine, this, _input);
        MoveState = new MoveState(StateMachine, this, _input);
        StateMachine.ChangeState(IdleState);
    }

    private void Update() => 
        StateMachine.CurrentState.Update();

    private void FixedUpdate() => 
        StateMachine.CurrentState.FixedUpdate();
}