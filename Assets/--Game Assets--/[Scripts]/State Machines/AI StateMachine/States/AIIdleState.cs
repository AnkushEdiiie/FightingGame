using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public AIIdleState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData) : base(AI, stateMachine, enemyData)
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

        MovementCalculation();

        if (AI.transform.localEulerAngles.y == 180)
        AI.AiAnim.SetFloat("VelocityX", AI.VelocityX);
        AI.AiAnim.SetFloat("VelocityZ", AI.VelocityZ);
        if (Vector3.Distance(AI.transform.position, AI.player.position) > 3 )
        {
            AI.StartCoroutine(startChangingState(AI.WalkState));
        }
        if (Vector3.Distance(AI.transform.position, AI.player.position) <= 1.5f && AI._damageHandler._stateHandlerPlayerA._stateMachine._currentState == AI._damageHandler._stateHandlerPlayerA._idleState)
        {
            int random = Random.Range(0, 4);

            if (random == 0)
            {
                stateMachine.ChangeState(AI.LightPunchState);
            }
            else if(random == 1)
            {
                stateMachine.ChangeState(AI.HardPunchState);
            }
            else
            {
                stateMachine.ChangeState(AI.LightKickState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void MovementCalculation()
    {
        if (AI.transform.localEulerAngles.y == 0)
        {
                if (AI.VelocityX > 0)
                    AI.VelocityX -= AI.decceleration * Time.deltaTime;
                else
                    AI.VelocityX += AI.decceleration * Time.deltaTime;
        
        }
        else if (AI.transform.localEulerAngles.y == 180)
        {  
                if (AI.VelocityX > 0)
                    AI.VelocityX -= AI.decceleration * Time.deltaTime;
                else
                    AI.VelocityX += AI.decceleration * Time.deltaTime;
        }
    }
    IEnumerator startChangingState(AIState state)
    {
        yield return new WaitForSeconds(enemyData.transitionDelay);
        stateMachine.ChangeState(state);
    }
}
