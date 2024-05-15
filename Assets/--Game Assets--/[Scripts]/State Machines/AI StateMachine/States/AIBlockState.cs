using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlockState : AIState
{
    public AIBlockState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData) : base(AI, stateMachine, enemyData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
