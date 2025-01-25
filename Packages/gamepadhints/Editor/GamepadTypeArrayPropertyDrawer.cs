using UnityEditor;
using BZ.Core.Collections.Editor;

namespace BZ.GamepadHints.Editor
{
    [CustomPropertyDrawer(typeof(GamepadTypeArray<>), true)]
    public class GamepadTypeArrayPropertyDrawer : EnumArrayPropertyDrawer<GamepadType> { }
}
