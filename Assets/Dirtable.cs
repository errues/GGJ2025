using System;
using UnityEngine;

public abstract class Dirtable : MonoBehaviour
{
    protected int _dirtLevel; // 0 = clean, 2 = dirty
    protected int MinDirtLevel => 0;
    protected abstract int MaxDirtLevel { get; }

    public bool IsFullDirty => _dirtLevel >= MaxDirtLevel;

    public int HygieneAddedByReducing = 1;
    public int HygieneAddedByCleaning = 2;
    public int HygieneReducedByDirt = 1;

    protected abstract void UpdateView();
    public void ReduceDirtLevel()
    {
        _dirtLevel = Math.Clamp(_dirtLevel - 1, MinDirtLevel, MaxDirtLevel);
        if (_dirtLevel == MinDirtLevel)
        {
            MessageBus.Instance.Notify("AddHygiene", HygieneAddedByCleaning);
        }
        else
        {
            MessageBus.Instance.Notify("AddHygiene", HygieneAddedByReducing);
        }

        UpdateView();
    }

    public void IncreaseDirtLevel()
    {
        if (_dirtLevel < MaxDirtLevel)
        {
            _dirtLevel = Math.Clamp(_dirtLevel + 1, MinDirtLevel, MaxDirtLevel);
            MessageBus.Instance.Notify("ReduceHygiene", HygieneReducedByDirt);
            UpdateView();
        }
    }

}
