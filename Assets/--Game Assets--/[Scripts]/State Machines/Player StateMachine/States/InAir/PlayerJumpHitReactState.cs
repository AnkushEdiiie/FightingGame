using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpHitReactState : PlayerState
{
    public PlayerJumpHitReactState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetTrigger("JumpHitReactTrigger");
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.ResetTrigger("JumpHitReactTrigger");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_animationController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            _stateMachine.ChangeState(_stateHandler._idleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
