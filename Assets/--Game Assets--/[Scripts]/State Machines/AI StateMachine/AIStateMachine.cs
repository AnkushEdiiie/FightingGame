
public class AIStateMachine
{
    public AIState CurrentState { get; private set; }
    public void Initialize(AIState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(AIState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();

    }
}
