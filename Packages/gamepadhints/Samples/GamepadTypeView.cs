using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BZ.GamepadHints.Test
{
    internal class GamepadTypeView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _gamepadTypeText;

        private void OnEnable()
        {
            InputChanged(InputChangeController.CurrentInputType, InputChangeController.CurrentGamepadType);
            InputChangeController.OnInputTypeChanged += InputChanged;
            InputChangeController.OnGamepadTypeChanged += GamepadChanged;
        }

        private void OnDisable()
        {
            InputChangeController.OnInputTypeChanged -= InputChanged;
            InputChangeController.OnGamepadTypeChanged -= GamepadChanged;
        }

        private void InputChanged(InputType inputType, GamepadType gamepadType)
        {
            if (inputType == InputType.GAMEPAD)
                _gamepadTypeText.text = inputType.ToString() + " - " + gamepadType.ToString();
            else
                _gamepadTypeText.text = inputType.ToString();
        }

        private void GamepadChanged(GamepadType gamepadType)
        {
            InputChanged(InputChangeController.CurrentInputType, gamepadType);
        }
    }
}
