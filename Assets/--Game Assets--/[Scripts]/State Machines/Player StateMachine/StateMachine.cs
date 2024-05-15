using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PlayerState _currentState;
    public PlayerState _previousState;

    public void InitializedState(PlayerState initialState)
    {
        _currentState = initialState;
        _previousState = initialState;
        _currentState.Enter();
    }
    public void ChangeState(PlayerState newState)
    {
        _previousState = _currentState;
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
