using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public class EditorGUILayoutStatViewElement<T> : EditorGUILayoutElement
    {
        private static readonly float _itemsPadding = 4f;
        private static readonly RectOffset _boxPadding = EditorStyles.helpBox.padding;

        private readonly SerializedProperty _statViewSerializedProperty;
        private readonly Action _buttonAction;

        public EditorGUILayoutStatViewElement(SerializedProperty statViewSerializedProperty, Action buttonAction)
        {
            _statViewSerializedProperty = statViewSerializedProperty;
            _buttonAction = buttonAction;
        }

        public override void Draw()
        {
            SerializedProperty stat = _statViewSerializedProperty.FindPropertyRelative("_stat");

            FieldInfo statField = typeof(T).GetField(stat.stringValue, BindingFlags.NonPublic | BindingFlags.Instance);


            GUI.Box(EditorGUI.IndentedRect(Rect), GUIContent.none, GUI.skin.box);

            //horizontal
            EditorGUIHorizontalLayout<NullEditorGUILayoutElement> horizontalLayout
                = new EditorGUIHorizontalLayout<NullEditorGUILayoutElement>(Rect.x, Rect.y);

            float buttonWidth = 25f + _itemsPadding + _boxPadding.horizontal;
            float availableWidth = Rect.width - buttonWidth;
            Rect labelRect = horizontalLayout.AddElement(availableWidth, Rect.height, new NullEditorGUILayoutElement()).Rect;
            labelRect = GUI.skin.box.padding.Remove(labelRect);

            Rect buttonRect = horizontalLayout.AddElement(buttonWidth, Rect.height, new NullEditorGUILayoutElement()).Rect;
            buttonRect = GUI.skin.box.padding.Remove(buttonRect);

            if (statField != null)
            {
                GUIContent itemLabel = new GUIContent(ObjectNames.NicifyVariableName(stat.stringValue));
                EditorGUI.LabelField(labelRect, itemLabel);
            }
            else
            {
                EditorGUI.LabelField(Rect, stat.stringValue + " - Value not found!!");
            }

            if (GUI.Button(buttonRect, new GUIContent("-", "Remove element")))
            {
                _buttonAction.Invoke();
            }
        }
    }
}