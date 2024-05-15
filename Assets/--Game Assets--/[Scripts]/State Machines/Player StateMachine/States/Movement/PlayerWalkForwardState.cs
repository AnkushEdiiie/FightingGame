using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkForwardState : PlayerState
{
    public PlayerWalkForwardState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetBool("Walk Forward", true);
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.SetBool("Walk Forward", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputReader._inputReaderHolder.lightPunch)
            _stateMachine.ChangeState(_stateHandler._jabState);

        else if (_inputReader._inputReaderHolder.hardPunch)
            _stateMachine.ChangeState(_stateHandler._punchState);

        else if (_inputReader._inputReaderHolder.lightKick)
            _stateMachine.ChangeState(_stateHandler._kickState);

        else if (_inputReader._inputReaderHolder.hardKick)
            _stateMachine.ChangeState(_stateHandler._axeKickState);

        else if (_inputReader._inputReaderHolder.crouch)
            _stateMachine.ChangeState(_stateHandler._crouchState);

        else if (_inputReader._inputReaderHolder.walkForward == false)
            _stateMachine.ChangeState(_stateHandler._idleState);

        else if (_inputReader._inputReaderHolder.jumpForward)
            _stateMachine.ChangeState(_stateHandler._jumpForwardState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
