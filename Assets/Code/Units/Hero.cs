using Zenject;

public class Hero : Character
{
    public StateMachine StateMachine;
    public IdleState IdleState;
    public MoveState MoveState;
    
    private IInputService _input;

    public CharacterMovement Movement => _movement;

    [Inject]
    public void Construct(IInputService input) => 
        _input = input;

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