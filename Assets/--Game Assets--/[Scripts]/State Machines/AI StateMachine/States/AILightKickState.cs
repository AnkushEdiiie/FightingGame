using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILightKickState : AIState
{

    private bool changingStateCoroutineRunning = false;
    public AILightKickState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData) : base(AI, stateMachine, enemyData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        AI.AiAnim.SetTrigger("lightKick");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!AI.isAttacking)
        {
            if (!changingStateCoroutineRunning)
            {
                AI.StartCoroutine(startChangingState(AI.IdleState));
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator startChangingState(AIState state)
    {
        changingStateCoroutineRunning = true;
        yield return new WaitForSeconds(enemyData.transitionDelay);
        stateMachine.ChangeState(state);
        changingStateCoroutineRunning = false;
    }
}