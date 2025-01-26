using System;
using UnityEngine;

public abstract class Dirtable : MonoBehaviour
{
    protected int _dirtLevel; // 0 = clean, 2 = dirty
    protected int MinDirtLevel => 0;
    protected abstract int MaxDirtLevel { get; }

    public bool IsFullDirty => _dirtLevel >= MaxDirtLevel;

    protected abstract void UpdateView();
    public void ReduceDirtLevel()
    {
        _dirtLevel = Math.Clamp(_dirtLevel - 1, MinDirtLevel, MaxDirtLevel);
        UpdateView();
    }

    public void IncreaseDirtLevel()
    {
        if (_dirtLevel < MaxDirtLevel)
        {
            _dirtLevel = Math.Clamp(_dirtLevel + 1, MinDirtLevel, MaxDirtLevel);
            UpdateView();
        }
    }

}
