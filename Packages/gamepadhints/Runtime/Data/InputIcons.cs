using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BZ.GamepadHints
{
    [CreateAssetMenu(fileName = "InputIcons", menuName = "ScriptableObjects/BZ/GamepadHints/Input Icons")]
    public class InputIcons : ScriptableObject
    {
        [System.Serializable]
        public class InputIconData
        {
#if UTILS_ATTRIBUTES
            [SpritePreview]
#endif
            [SerializeField]
            private Sprite _icon;
            [SerializeField]
            private string _textMeshProIcon;

            public Sprite Icon => _icon;
            public string TextMeshProIcon => _textMeshProIcon;
        }

        public const string SpriteAssetNameGamepad = "controlsGamepad";
        public const string SpriteAssetNameKeyboard = "controlsKeyboard";

        [SerializeField]
        private GamepadButtonArray<GamepadTypeArray<InputIconData>> _gamepadButtonIcons;

        [Header("Controls Sprite Assets")]
        [SerializeField]
        private TMP_SpriteAsset _controlsSpriteAsset;

        [Header("Controls Keyboard Sprite Assets")]
        [SerializeField]
        private TMP_SpriteAsset _controlsKeyboardSpriteAsset;


        public InputIconData GetInputIcon(GamepadButton gamepadButton, GamepadType gamepadType)
        {
            return _gamepadButtonIcons[gamepadButton][gamepadType];
        }

        public static InputIcons GetInputIconsAsset()
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(InputIcons)}");
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                if (guids.Length > 1)
                {
                    Debug.LogError("There are more than one InpurIcons asset!! Getting Icons from: " + assetPath);
                }

                return AssetDatabase.LoadAssetAtPath<InputIcons>(assetPath);
            }
#endif

            return null;
        }


        // TMP require asset Request
        private void OnEnable()
        {
            TMP_Text.OnSpriteAssetRequest -= ProcessSpriteAssetRequest;
            TMP_Text.OnSpriteAssetRequest += ProcessSpriteAssetRequest;
        }

        private void OnDisable()
        {
            TMP_Text.OnSpriteAssetRequest -= ProcessSpriteAssetRequest;
        }

        private void OnDestroy()
        {
            TMP_Text.OnSpriteAssetRequest -= ProcessSpriteAssetRequest;
        }

        private TMP_SpriteAsset ProcessSpriteAssetRequest(int hashCode, string code)
        {
            if (code == SpriteAssetNameGamepad)
            {
                return _controlsSpriteAsset;
            }

            if (code == SpriteAssetNameKeyboard)
            {
                return _controlsKeyboardSpriteAsset;
            }

            return null;
        }

    }

}