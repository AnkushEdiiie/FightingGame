using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
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

        if (_inputReader._inputReaderHolder.walkForward)
            _stateMachine.ChangeState(_stateHandler._walkForwardState);

        else if (_inputReader._inputReaderHolder.walkBackward)
            _stateMachine.ChangeState(_stateHandler._walkBackwardState);

        else if(_inputReader._inputReaderHolder.lightPunch)
            _stateMachine.ChangeState(_stateHandler._jabState);

        else if (_inputReader._inputReaderHolder.hardPunch)
            _stateMachine.ChangeState(_stateHandler._punchState);

        else if (_inputReader._inputReaderHolder.lightKick)
            _stateMachine.ChangeState(_stateHandler._kickState);

        else if (_inputReader._inputReaderHolder.hardKick)
            _stateMachine.ChangeState(_stateHandler._axeKickState);

        else if (_inputReader._inputReaderHolder.crouch)
            _stateMachine.ChangeState(_stateHandler._crouchState);

        else if (_inputReader._inputReaderHolder.jump)
            _stateMachine.ChangeState(_stateHandler._jumpState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}