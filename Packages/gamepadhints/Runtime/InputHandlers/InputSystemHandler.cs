#if INPUT_SYSTEM
using System.Linq;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch;

namespace BZ.GamepadHints
{
    public class InputSystemHandler : InputHandler
    {
        private InputDevice _lastUsedDevice;

        public InputSystemHandler(InputChangeController inputChangeController) : base(inputChangeController)
        {
        }

        public override void AddInputChangeEvents()
        {
            InputSystem.onEvent += OnInputSystemEvent;
        }

        public override void RemoveInputChangeEvents()
        {
            InputSystem.onEvent -= OnInputSystemEvent;
        }

        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (_lastUsedDevice == device)
                return;

            // Some devices like to spam events like crazy.
            // Example: PS4 controller on PC keeps triggering events without meaningful change.
            var eventType = eventPtr.type;
            if (eventType == StateEvent.Type)
            {
                // Go through the changed controls in the event and look for ones actuated
                // above a magnitude of a little above zero.
                if (!eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.0001f).Any())
                    return;
            }

            if (device is Mouse || device is Keyboard)
            {
                //Higher threshold for mouse input
                if (device is Mouse && !eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.1f).Any())
                    return;

                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;

                //_inputChangeController.ChangeInputType(InputType.MOUSE_KEYBOARD);
                _inputChangeController.ChangeInputType(InputType.GAMEPAD);
                _inputChangeController.ChangeGamepadType(GamepadType.MOUSE_KEYBOARD);
            }
            else
            {
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;

                if (device is SwitchProControllerHID)
                {
                    _inputChangeController.ChangeGamepadType(GamepadType.SWITCH);
                }
                else if (device is DualShock4GamepadHID)
                {
                    _inputChangeController.ChangeGamepadType(GamepadType.PS4);
                }
                else if (device is DualSenseGamepadHID)
                {
                    _inputChangeController.ChangeGamepadType(GamepadType.PS5);
                }
                else
                {
                    _inputChangeController.ChangeGamepadType(GamepadType.XBOX);
                }

                _inputChangeController.ChangeInputType(InputType.GAMEPAD);
            }

            _lastUsedDevice = device;
        }
    }
}
#endif
