using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeKickState : PlayerState
{
    public PlayerAxeKickState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetTrigger("AxeKickTrigger");
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.ResetTrigger("AxeKickTrigger");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_animationController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.94f && _animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("AxeKick")/*_inputReader._inputReaderHolder.hardKick == false*/)
            _stateMachine.ChangeState(_stateHandler._idleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
