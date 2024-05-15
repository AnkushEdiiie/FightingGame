using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected StateMachine _stateMachine;
    protected StateHandler _stateHandler;
    protected InputReader _inputReader;
    protected AnimationController _animationController;
    protected float _stateTimer;

    public PlayerState(StateHandler stateHandler, StateMachine stateMachine, InputReader inputReader, AnimationController animationController)
    {
        this._stateHandler = stateHandler;
        this._stateMachine = stateMachine;
        this._inputReader = inputReader;
        this._animationController = animationController;
    }
    public virtual void Enter()
    {
        DoCheck();
        _stateTimer = Time.time;
        Debug.Log(_stateMachine._currentState);
    }
    public virtual void Exit()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }
    public virtual void DoCheck()
    {

    }
}
