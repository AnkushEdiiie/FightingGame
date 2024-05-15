using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJabState : PlayerState
{
    public PlayerJabState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController) : base(stateHandler, stateMachine, inputReader, animationController)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        _animationController.animator.SetTrigger("JabTrigger");
    }

    public override void Exit()
    {
        base.Exit();
        _animationController.animator.ResetTrigger("JabTrigger");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_inputReader._inputReaderHolder.hardPunch)
            _stateMachine.ChangeState(_stateHandler._uppercutState);

        else if (_animationController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.94f && _animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("Jab")/*_inputReader._inputReaderHolder.lightPunch == false*/)
            _stateMachine.ChangeState(_stateHandler._idleState);

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
