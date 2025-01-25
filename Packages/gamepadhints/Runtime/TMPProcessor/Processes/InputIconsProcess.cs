using System;
using System.Text.RegularExpressions;
using BZ.GamepadHints;
using UnityEngine;

[System.Serializable]
public class InputIconsProcess : TMPBaseProcess
{
    private const string _replaceText = "<sprite=\"{0}\" index={1}>";

    private InputIcons inputIcons;

    public override string ProcessText(string textToProcess, MatchCollection textVariables)
    {
        inputIcons = InputChangeController.InputIcons;
#if UNITY_EDITOR
        if (!Application.isPlaying && inputIcons == null)
        {
            inputIcons = InputIcons.GetInputIconsAsset();
        }
#endif

        foreach (Match match in textVariables)
        {
            GamepadButton gamepadButton;
            if (Enum.TryParse(match.Groups[1].Value, true, out gamepadButton))
            {
                if(InputChangeController.CurrentGamepadType == GamepadType.MOUSE_KEYBOARD)
                    textToProcess = textToProcess.Replace(match.Groups[0].Value, string.Format(_replaceText, InputIcons.SpriteAssetNameKeyboard, inputIcons.GetInputIcon(gamepadButton, InputChangeController.CurrentGamepadType).TextMeshProIcon));
                else
                    textToProcess = textToProcess.Replace(match.Groups[0].Value, string.Format(_replaceText, InputIcons.SpriteAssetNameGamepad, inputIcons.GetInputIcon(gamepadButton, InputChangeController.CurrentGamepadType).TextMeshProIcon));
            }
        }

        return textToProcess;
    }
}
