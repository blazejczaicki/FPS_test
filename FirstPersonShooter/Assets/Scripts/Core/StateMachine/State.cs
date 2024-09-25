public abstract class State//<T> where T : Enum
{
    protected StateMachine _stateMachine;

    //public T StateName { get; protected set; }

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void CheckChangeState();
    public abstract void OnUpdate();
    public abstract void OnEnter();
    public abstract void OnExit();

}
