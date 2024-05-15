using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    public PlayerCrouchState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetBool("Crouch", true);
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.SetBool("Crouch", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_inputReader._inputReaderHolder.crouch == false)
            _stateMachine.ChangeState(_stateHandler._idleState);

        if (_inputReader._inputReaderHolder.lightPunch)
            _stateMachine.ChangeState(_stateHandler._lowPunchState);

        if(_inputReader._inputReaderHolder.hardPunch)
            _stateMachine.ChangeState(_stateHandler._sweepState);

        if(_inputReader._inputReaderHolder.lightKick)
            _stateMachine.ChangeState(_stateHandler._lowKickState);

        if(_inputReader._inputReaderHolder.hardKick)
            _stateMachine.ChangeState(_stateHandler._downSmashState);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
