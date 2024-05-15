using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKnockdownState : AIState
{
    public AIKnockdownState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData) : base(AI, stateMachine, enemyData)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        AI.AiAnim.SetTrigger("knockdown");
        Debug.Log("Enter into knowkdown hit state");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(AI.AiAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            stateMachine.ChangeState(AI.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
