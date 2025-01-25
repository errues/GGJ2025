#if REWIRED
using System.Collections.Generic;
using Rewired;

namespace BZ.GamepadHints
{
    public class RewiredInputHandler : InputHandler
    {
        private static Dictionary<string, GamepadType> RewiredGamepads = new Dictionary<string, GamepadType>()
    {
        { "cd9718bf-a87a-44bc-8716-60a0def28a9f", GamepadType.PS4 }, // DualShock4
        { "5286706d-19b4-4a45-b635-207ce78d8394", GamepadType.PS5 }, // DualSense
        { "d74a350e-fe8b-4e9e-bbcd-efff16d34115", GamepadType.XBOX }, // Xbox 360 Controller
        { "19002688-7406-4f4a-8340-8d25335406c8", GamepadType.XBOX }, // Xbox One Controller
        { "3eb01142-da0e-4a86-8ae8-a15c2b1f2a04", GamepadType.SWITCH }, // Joy-Con L
        { "605dc720-1b38-473d-a459-67d5857aa6ea", GamepadType.SWITCH }, // Joy-Con R
        { "521b808c-0248-4526-bc10-f1d16ee76bf1", GamepadType.SWITCH }, // Joy-Con Dual
        { "1fbdd13b-0795-4173-8a95-a2a75de9d204", GamepadType.SWITCH }, // Joy-Con Handheld
        { "7bf3154b-9db8-4d52-950f-cd0eed8a5819", GamepadType.SWITCH }, // Pro Controller
    };

        private static Dictionary<GamepadType, Dictionary<int, GamepadButton>> RewiredGamepadsButons = new Dictionary<GamepadType, Dictionary<int, GamepadButton>>()
    {
        {   // DualShock4
            GamepadType.PS4,
            new Dictionary<int, GamepadButton>() {
                { 6, GamepadButton.SOUTH_BUTTON },
                { 7, GamepadButton.EAST_BUTTON },
                { 8, GamepadButton.WEST_BUTTON },
                { 9, GamepadButton.NORTH_BUTTON },
                { 10, GamepadButton.LEFT_SHOULDER },
                { 4, GamepadButton.LEFT_TRIGGER },
                { 11, GamepadButton.RIGHT_SHOULDER },
                { 5, GamepadButton.RIGHT_TRIGGER },
                { 13, GamepadButton.START_BUTTON },
                { 15, GamepadButton.SELECT_BUTTON },
                { 16, GamepadButton.LEFT_JOYSTICK_BUTTON },
                { 17, GamepadButton.RIGHT_JOYSTICK_BUTTON },
                { 18, GamepadButton.DPAD_UP },
                { 19, GamepadButton.DPAD_RIGHT },
                { 20, GamepadButton.DPAD_DOWN },
                { 21, GamepadButton.DPAD_LEFT },
                { 22, GamepadButton.LEFT_JOYSTICK },
                { 23, GamepadButton.RIGHT_JOYSTICK }
            }
        },
        {   // DualSense
            GamepadType.PS5,
            new Dictionary<int, GamepadButton>() {
                { 6, GamepadButton.SOUTH_BUTTON },
                { 7, GamepadButton.EAST_BUTTON },
                { 8, GamepadButton.WEST_BUTTON },
                { 9, GamepadButton.NORTH_BUTTON },
                { 10, GamepadButton.LEFT_SHOULDER },
                { 4, GamepadButton.LEFT_TRIGGER },
                { 11, GamepadButton.RIGHT_SHOULDER },
                { 5, GamepadButton.RIGHT_TRIGGER },
                { 13, GamepadButton.START_BUTTON },
                { 15, GamepadButton.SELECT_BUTTON },
                { 16, GamepadButton.LEFT_JOYSTICK_BUTTON },
                { 17, GamepadButton.RIGHT_JOYSTICK_BUTTON },
                { 18, GamepadButton.DPAD_UP },
                { 19, GamepadButton.DPAD_RIGHT },
                { 20, GamepadButton.DPAD_DOWN },
                { 21, GamepadButton.DPAD_LEFT },
                { 22, GamepadButton.LEFT_JOYSTICK },
                { 23, GamepadButton.RIGHT_JOYSTICK }
            }
        },
        {   // Xbox 360/One Controller
            GamepadType.XBOX,
            new Dictionary<int, GamepadButton>() {
                { 6, GamepadButton.SOUTH_BUTTON },
                { 7, GamepadButton.EAST_BUTTON },
                { 8, GamepadButton.WEST_BUTTON },
                { 9, GamepadButton.NORTH_BUTTON },
                { 10, GamepadButton.LEFT_SHOULDER },
                { 4, GamepadButton.LEFT_TRIGGER },
                { 11, GamepadButton.RIGHT_SHOULDER },
                { 5, GamepadButton.RIGHT_TRIGGER },
                { 13, GamepadButton.START_BUTTON },
                { 12, GamepadButton.SELECT_BUTTON },
                { 14, GamepadButton.LEFT_JOYSTICK_BUTTON },
                { 15, GamepadButton.RIGHT_JOYSTICK_BUTTON },
                { 16, GamepadButton.DPAD_UP },
                { 17, GamepadButton.DPAD_RIGHT },
                { 18, GamepadButton.DPAD_DOWN },
                { 19, GamepadButton.DPAD_LEFT },
                { 20, GamepadButton.LEFT_JOYSTICK },
                { 21, GamepadButton.RIGHT_JOYSTICK }
            }
        },
        {   // Joy-Con Dual / Pro Controller
            GamepadType.SWITCH,
            new Dictionary<int, GamepadButton>() {
                { 4, GamepadButton.SOUTH_BUTTON },
                { 5, GamepadButton.EAST_BUTTON },
                { 6, GamepadButton.WEST_BUTTON },
                { 7, GamepadButton.NORTH_BUTTON },
                { 8, GamepadButton.LEFT_SHOULDER },
                { 10, GamepadButton.LEFT_TRIGGER },
                { 9, GamepadButton.RIGHT_SHOULDER },
                { 11, GamepadButton.RIGHT_TRIGGER },
                { 13, GamepadButton.START_BUTTON },
                { 12, GamepadButton.SELECT_BUTTON },
                { 16, GamepadButton.LEFT_JOYSTICK_BUTTON },
                { 17, GamepadButton.RIGHT_JOYSTICK_BUTTON },
                { 18, GamepadButton.DPAD_UP },
                { 19, GamepadButton.DPAD_RIGHT },
                { 20, GamepadButton.DPAD_DOWN },
                { 21, GamepadButton.DPAD_LEFT },
                { 22, GamepadButton.LEFT_JOYSTICK },
                { 23, GamepadButton.RIGHT_JOYSTICK }
            }
        },
    };

        public RewiredInputHandler(InputChangeController inputChangeController) : base(inputChangeController)
        {
        }

        public override void AddInputChangeEvents()
        {
            ReInput.controllers.RemoveLastActiveControllerChangedDelegate(CheckActiveController);
            ReInput.controllers.AddLastActiveControllerChangedDelegate(CheckActiveController);
        }

        public override void RemoveInputChangeEvents()
        {
            if (ReInput.isReady && ReInput.controllers != null)
                ReInput.controllers.RemoveLastActiveControllerChangedDelegate(CheckActiveController);
        }

        private void CheckActiveController(Controller controller)
        {
            switch (controller.type)
            {
                case ControllerType.Keyboard:
                case ControllerType.Mouse:
                    _inputChangeController.ChangeInputType(InputType.MOUSE_KEYBOARD);
                    break;
                case ControllerType.Joystick:
                case ControllerType.Custom:
                    if (RewiredGamepads.ContainsKey(controller.hardwareTypeGuid.ToString()))
                    {
                        _inputChangeController.ChangeGamepadType(RewiredGamepads[controller.hardwareTypeGuid.ToString()]);
                    }
                    else
                    {
                        _inputChangeController.ChangeGamepadType(InputChangeController.DefaultPlatformGamepad);
                    }

                    _inputChangeController.ChangeInputType(InputType.GAMEPAD);
                    break;
            }
        }


        public static GamepadButton GetButtonSpriteKey(int buttonId)
        {
            return GetButtonSpriteKey(InputChangeController.CurrentGamepadType, buttonId);
        }

        public static GamepadButton GetButtonSpriteKey(GamepadType platform, int buttonId)
        {
            if (!RewiredGamepadsButons.ContainsKey(platform) || !RewiredGamepadsButons[platform].ContainsKey(buttonId))
                return GamepadButton.SOUTH_BUTTON;
            return RewiredGamepadsButons[platform][buttonId];
        }
    }
}
#endif