using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public class EditorGUILayoutModStatElement<T> : EditorGUILayoutElement
    {
        private static readonly float _itemsPadding = 4f;
        private static readonly RectOffset _boxPadding = EditorStyles.helpBox.padding;

        private readonly SerializedProperty _statModSerializedProperty;
        private readonly Action _buttonAction;

        public EditorGUILayoutModStatElement(SerializedProperty statModSerializedProperty, Action buttonAction)
        {
            _statModSerializedProperty = statModSerializedProperty;
            _buttonAction = buttonAction;
        }

        public override void Draw()
        {
            SerializedProperty stat = _statModSerializedProperty.FindPropertyRelative("stat");
            SerializedProperty statOperation = _statModSerializedProperty.FindPropertyRelative("operation");
            SerializedProperty statFloatValue = _statModSerializedProperty.FindPropertyRelative("floatValue");
            SerializedProperty statBoolValue = _statModSerializedProperty.FindPropertyRelative("boolValue");

            FieldInfo statField = typeof(T).GetField(stat.stringValue, BindingFlags.NonPublic | BindingFlags.Instance);


            GUI.Box(EditorGUI.IndentedRect(Rect), GUIContent.none, GUI.skin.box);

            //horizontal
            EditorGUIHorizontalLayout<NullEditorGUILayoutElement> horizontalLayout
                = new EditorGUIHorizontalLayout<NullEditorGUILayoutElement>(Rect.x, Rect.y);

            float buttonWidth = 25f + _itemsPadding + _boxPadding.horizontal;
            float availableWidth = Rect.width - buttonWidth;
            Rect labelRect = horizontalLayout.AddElement(availableWidth * 0.5f, Rect.height, new NullEditorGUILayoutElement()).Rect;
            labelRect = GUI.skin.box.padding.Remove(labelRect);

            Rect statOperationRect = horizontalLayout.AddElement(availableWidth * 0.3f, Rect.height, new NullEditorGUILayoutElement()).Rect;
            statOperationRect = GUI.skin.box.padding.Remove(statOperationRect);

            Rect valueRect = horizontalLayout.AddElement(availableWidth * 0.2f, Rect.height, new NullEditorGUILayoutElement()).Rect;
            valueRect = GUI.skin.box.padding.Remove(valueRect);

            Rect buttonRect = horizontalLayout.AddElement(buttonWidth, Rect.height, new NullEditorGUILayoutElement()).Rect;
            buttonRect = GUI.skin.box.padding.Remove(buttonRect);

            if (statField != null)
            {
                Type statType = statField.FieldType;
                bool statTypeIsBool = statType == typeof(bool);
                GUIContent itemLabel = new GUIContent(ObjectNames.NicifyVariableName(stat.stringValue));

                EditorGUI.LabelField(labelRect, itemLabel);
                EditorGUI.PropertyField(statOperationRect, statOperation, GUIContent.none);

                if (statTypeIsBool)
                {
                    EditorGUI.PropertyField(valueRect, statBoolValue, GUIContent.none);
                }
                else
                {
                    EditorGUI.PropertyField(valueRect, statFloatValue, GUIContent.none);
                }
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