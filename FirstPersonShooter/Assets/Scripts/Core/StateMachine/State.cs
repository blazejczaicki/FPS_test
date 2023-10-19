using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
    public State(StateMachine stateMachine)
    {
        _stateMachine=stateMachine;
    }

    public abstract void CheckChangeState();
    public abstract void OnUpdate();
    public abstract void OnEnter();
    public abstract void OnExit();

}
