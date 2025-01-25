using UnityEngine;

public abstract class State
{
    protected StateMachine StateMachine;

    protected State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {
        Debug.Log($"<color=green> ## ENTER {this.GetType().Name}</color>");
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnExit()
    {
        Debug.Log($"<color=orange> ## EXIT {this.GetType().Name}</color>");
    }
}
