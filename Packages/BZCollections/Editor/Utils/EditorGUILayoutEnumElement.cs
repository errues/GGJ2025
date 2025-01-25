using UnityEditor;
using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public class EditorGUILayoutEnumElement : EditorGUILayoutElement
    {
        private readonly SerializedProperty _serializedProperty;
        private readonly string _enumValue;

        public EditorGUILayoutEnumElement(SerializedProperty serializedProperty, string enumValue)
        {
            _serializedProperty = serializedProperty;
            _enumValue = enumValue;
        }

        public override void Draw()
        {
            GUI.Box(EditorGUI.IndentedRect(Rect), GUIContent.none, GUI.skin.box);
            Rect = GUI.skin.box.padding.Remove(Rect);
            EditorGUI.indentLevel++;
            GUIContent arrayItemLabel = new GUIContent(_enumValue);
            EditorGUI.PropertyField(Rect, _serializedProperty, arrayItemLabel, true);
            EditorGUI.indentLevel--;
        }
    }
}