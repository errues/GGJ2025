using UnityEditor;
using BZ.Core.Collections.Editor;

namespace BZ.GamepadHints.Editor
{
    [CustomPropertyDrawer(typeof(GamepadButtonArray<>), true)]
    public class GamepadButtonArrayPropertyDrawer : EnumArrayPropertyDrawer<GamepadButton> { }
}
