
using UnityEngine;

public class AIState
{
    protected AI_StateHandler AI;
    protected AIStateMachine stateMachine;
    protected AIData enemyData;

    protected float startTime;

    public AIState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData)
    {
        this.AI = AI;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
    }
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        Debug.Log(stateMachine.CurrentState);
    }
    public virtual void Exit()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}

