using UnityEngine;

#if INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace BZ.GamepadHints
{
    [AddComponentMenu("Brave Zebra/Input/Input Change Controller")]
    public class InputChangeController : MonoBehaviour
    {
        public static InputIcons InputIcons { get; set; }

        private static InputType _currentInputType;
        public static InputType CurrentInputType { get => _currentInputType; private set { _currentInputType = value; } }

        private static GamepadType _currentGamepadType;
        public static GamepadType CurrentGamepadType { get => _currentGamepadType; private set { _currentGamepadType = value; } }

        public static System.Action<InputType, GamepadType> OnInputTypeChanged { get; set; }
        public static System.Action<GamepadType> OnGamepadTypeChanged { get; set; }

        public static InputType DefaultPlatformInput
        {
            get
            {
#if UNITY_GAMECORE || UNITY_SWITCH || UNITY_PS4 || UNITY_PS5
                return InputType.GAMEPAD;
#elif UNITY_ANDROID || UNITY_IOS
                return InputType.TOUCHPAD;
#else
                return InputType.GAMEPAD;
#endif
            }
        }

        public static GamepadType DefaultPlatformGamepad
        {
            get
            {
#if UNITY_GAMECORE
                return GamepadType.XBOX;
#elif UNITY_SWITCH
                return GamepadType.SWITCH;
#elif UNITY_PS4
                return GamepadType.PS4;
#elif UNITY_PS5
                return GamepadType.PS5;
#else
                return GamepadType.MOUSE_KEYBOARD;
#endif
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize()
        {
            _currentInputType = DefaultPlatformInput;
            _currentGamepadType = DefaultPlatformGamepad;
        }


        [Header("Icons")]
        [SerializeField] private InputIcons _inputIcons;

        private bool _lockedChange = false;

        private InputHandler _inputHandler;
        public InputHandler InputHandler
        {
            get
            {
                if (_inputHandler == null)
                {
#if INPUT_SYSTEM
                    _inputHandler = new InputSystemHandler(this);
#elif REWIRED
                    _inputHandler = new RewiredInputHandler(this);
#endif
                }

                return _inputHandler;
            }

            private set
            {

            }
        }

        private void OnEnable()
        {
            InputHandler.AddInputChangeEvents();
        }

        private void OnDisable()
        {
            if (!_lockedChange && _inputHandler != null)
            {
                InputHandler.RemoveInputChangeEvents();
            }
        }

        private void Awake()
        {
            if (InputIcons != _inputIcons)
                InputIcons = _inputIcons;
        }

        public void SetCustomInputHandler(InputHandler inputHandler)
        {
            if (gameObject.activeInHierarchy && _inputHandler != null)
            {
                InputHandler.RemoveInputChangeEvents();
            }

            _inputHandler = inputHandler;

            if (gameObject.activeInHierarchy)
            {
                _inputHandler.AddInputChangeEvents();
            }
        }

        public void ChangeInputType(InputType newInputType)
        {
            if (newInputType != _currentInputType)
            {
                Debug.Log("#BZ# :: Input type changed to " + newInputType.ToString());
                _currentInputType = newInputType;

                OnInputTypeChanged?.Invoke(_currentInputType, _currentGamepadType);
            }
        }

        public void ChangeGamepadType(GamepadType newGamepadType)
        {
            if (newGamepadType != _currentGamepadType)
            {
                Debug.Log("#BZ# :: Gamepad type changed to " + newGamepadType.ToString());
                _currentGamepadType = newGamepadType;

                OnGamepadTypeChanged?.Invoke(_currentGamepadType);
            }
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private void Update()
        {
#if INPUT_SYSTEM
            if (Keyboard.current.digit0Key.wasPressedThisFrame)
#else
            if (Input.GetKeyDown(KeyCode.Alpha0))
#endif
            {
                ChangeGamepadType(GamepadType.XBOX);
                OnEnable();
                _lockedChange = false;
            }
#if INPUT_SYSTEM
            else if (Keyboard.current.digit1Key.wasPressedThisFrame)
#else
            else if (Input.GetKeyDown(KeyCode.Alpha1))
#endif
            {
                ChangeGamepadType(GamepadType.XBOX);
                ChangeInputType(InputType.GAMEPAD);
                OnDisable();
                _lockedChange = true;
            }
#if INPUT_SYSTEM
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
#else
            else if (Input.GetKeyDown(KeyCode.Alpha2))
#endif
            {
                ChangeGamepadType(GamepadType.SWITCH);
                ChangeInputType(InputType.GAMEPAD);
                OnDisable();
                _lockedChange = true;
            }
#if INPUT_SYSTEM
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
#else
            else if (Input.GetKeyDown(KeyCode.Alpha3))
#endif
            {
                ChangeGamepadType(GamepadType.PS4);
                ChangeInputType(InputType.GAMEPAD);
                OnDisable();
                _lockedChange = true;
            }
#if INPUT_SYSTEM
            else if (Keyboard.current.digit4Key.wasPressedThisFrame)
#else
            else if (Input.GetKeyDown(KeyCode.Alpha4))
#endif
            {
                ChangeGamepadType(GamepadType.PS5);
                ChangeInputType(InputType.GAMEPAD);
                OnDisable();
                _lockedChange = true;
            }
        }
#endif

        }
    }