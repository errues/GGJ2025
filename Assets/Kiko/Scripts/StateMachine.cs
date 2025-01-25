public class StateMachine
{
    private State _previousState;
    private State _currentState;

    public State CurrentState { get => _currentState; }

    public void Initialize(State defaultState)
    {
        _currentState = defaultState;
        _previousState = _currentState;

        defaultState.OnEnter();
    }

    public void ChangeState(State newState)
    {
        _currentState.OnExit();
        _previousState = _currentState;
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void BackToPreviousState()
    {
        ChangeState(_previousState);
    }
}
