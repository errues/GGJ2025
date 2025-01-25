using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BZ.GamepadHints
{
    [AddComponentMenu("Brave Zebra/Gamepad Hints/Gamepad Button Hint")]
    public class GamepadButtonHint : MonoBehaviour
    {
        [SerializeField]
        private GamepadButton _buttonTip = GamepadButton.SOUTH_BUTTON;

        [SerializeField]
        private Image _buttonTipImage = null;

        [SerializeField, HideInInspector]
        private GamepadButton _lastButtonTip = (GamepadButton)(-1);

        [ContextMenu("Update Editor Image")]
        protected void UpdateEditorImage()
        {
#if UNITY_EDITOR
            InputIcons asset = InputIcons.GetInputIconsAsset();
            if (asset != null)
            {
                _buttonTipImage.sprite = asset.GetInputIcon(_buttonTip, InputChangeController.CurrentGamepadType).Icon;
            }
#endif
            _lastButtonTip = _buttonTip;
        }

        private void OnValidate()
        {
            if (_buttonTipImage != null && _lastButtonTip != _buttonTip)
            {
                UpdateEditorImage();
            }
        }

        private void OnEnable()
        {
            InputChangeController.OnGamepadTypeChanged += UpdateButtonTip;

            if (InputChangeController.InputIcons != null)
            {
                UpdateButtonTip(InputChangeController.CurrentGamepadType);
            }
        }

        private void OnDisable()
        {
            InputChangeController.OnGamepadTypeChanged -= UpdateButtonTip;
        }

        private void UpdateButtonTip(GamepadType gamepadType)
        {
            _buttonTipImage.sprite = InputChangeController.InputIcons.GetInputIcon(_buttonTip, gamepadType).Icon;
        }
    }

}