using System;
using BZ.GamepadHints;

[System.Serializable]
public class InputTypeChange : TMPBaseProcessorTrigger
{
    public override void SetTrigger(Action processorAction)
    {
        InputChangeController.OnGamepadTypeChanged += (controller) => processorAction();
    }

    public override void RemoveTrigger(Action processorAction)
    {
        InputChangeController.OnGamepadTypeChanged -= (controller) => processorAction();
    }
}
