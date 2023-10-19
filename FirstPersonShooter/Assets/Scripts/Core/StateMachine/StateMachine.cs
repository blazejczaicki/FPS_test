using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _currentState;
    public State CurrentState => _currentState;

    protected void Update()
    {
        _currentState?.CheckChangeState();
        _currentState?.OnUpdate();
    }

    public virtual void ChangeState(State newState)
    {
        _currentState?.OnExit();

        _currentState = newState;
        _currentState?.OnEnter();
    }
}
