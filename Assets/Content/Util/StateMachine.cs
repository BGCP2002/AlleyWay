public class StateMachine<TState> where TState : IState
{
    private TState _state;

    public void SetState(TState state)
    {
        _state?.OnExit();
        _state = state;
        _state?.OnEnter();
    }

    public void Update()
    {
        _state?.Update();
    }

    public void FixedUpdate()
    {
        _state?.FixedUpdate();
    }
}

public interface IState
{
    public void OnEnter();
    public void OnExit();
    public void Update();
    public void FixedUpdate();
}