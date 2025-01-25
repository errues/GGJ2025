using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager
{
    private static GlobalManager _instance;
    public static GlobalManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GlobalManager();
            return _instance;
        }
    }

    // FSM
    private StateMachine _stateMachine;

    public GlobalManager()
    {
        InitFSM();
    }




    private void InitFSM()
    {
        _stateMachine = new();
    }


    public void UpdateStateMachine()
    {
        _stateMachine.CurrentState.OnUpdate();
    }
}
