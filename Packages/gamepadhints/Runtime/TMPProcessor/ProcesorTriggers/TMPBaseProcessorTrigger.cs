using System;
using UnityEngine;

[System.Serializable]
public abstract class TMPBaseProcessorTrigger
{
    public abstract void SetTrigger(Action processorAction);

    public abstract void RemoveTrigger(Action processorAction);
}
