using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AIWalkState : AIState
{
    public AIWalkState(AI_StateHandler AI, AIStateMachine stateMachine, AIData enemyData) : base(AI, stateMachine, enemyData)
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
        {
            AI.AiAnim.SetFloat("VelocityX", AI.VelocityX);
            AI.AiAnim.SetFloat("VelocityZ", AI.VelocityZ);
        }
        if (Vector3.Distance(AI.transform.position, AI.player.position) <= 3)
        {
            AI.StartCoroutine(startChangingState());
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
            if (AI.controller.velocity.z > 0)
            {
                AI.VelocityX += AI.acceleration * Time.deltaTime;
                if (AI.VelocityX > 1) AI.VelocityX = 1;
            }

            if (AI.controller.velocity.z == 0)
            {
                if (AI.VelocityX > 0)
                    AI.VelocityX -= AI.decceleration * Time.deltaTime;
                else
                    AI.VelocityX += AI.decceleration * Time.deltaTime;
            }

            if (AI.controller.velocity.z < 0)
            {
                AI.VelocityX -= AI.acceleration * Time.deltaTime;
                if (AI.VelocityX < -1) AI.VelocityX = -1;
            }
        }
        else if (AI.transform.localEulerAngles.y == 180)
        {
            if (AI.controller.velocity.z < 0)
            {
                AI.VelocityX += AI.acceleration * Time.deltaTime;
                if (AI.VelocityX > 1) AI.VelocityX = 1;
            }

            if (AI.controller.velocity.z == 0)
            {
                if (AI.VelocityX > 0)
                    AI.VelocityX -= AI.decceleration * Time.deltaTime;
                else
                    AI.VelocityX += AI.decceleration * Time.deltaTime;
            }

            if (AI.controller.velocity.z > 0)
            {
                AI.VelocityX -= AI.acceleration * Time.deltaTime;
                if (AI.VelocityX < -1) AI.VelocityX = -1;
            }
        }
    }
    IEnumerator startChangingState()
    {
        yield return new WaitForSeconds(enemyData.transitionDelay);
        stateMachine.ChangeState(AI.IdleState);
    }
}
