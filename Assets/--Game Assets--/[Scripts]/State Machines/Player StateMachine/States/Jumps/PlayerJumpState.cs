using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetTrigger("JumpTrigger");
        _animationController.CoRun();
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.ResetTrigger("JumpTrigger");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_inputReader._inputReaderHolder.hardPunch)
            _stateMachine.ChangeState(_stateHandler._highPunchState);

        else if (_inputReader._inputReaderHolder.hardKick)
            _stateMachine.ChangeState(_stateHandler._highKickState);

        else if (_inputReader._inputReaderHolder.jump == false)
            _stateMachine.ChangeState(_stateHandler._idleState);       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
