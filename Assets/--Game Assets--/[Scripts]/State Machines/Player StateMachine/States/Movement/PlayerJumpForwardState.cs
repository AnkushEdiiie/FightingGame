using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpForwardState : PlayerState
{
    public PlayerJumpForwardState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetTrigger("JumpForwardTrigger");
        _animationController.CoRun();
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.ResetTrigger("JumpForwardTrigger");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_animationController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            _stateMachine.ChangeState(_stateMachine._previousState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
