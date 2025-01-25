using System;
using UnityEditor;
using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public abstract class EnumArrayPropertyDrawer<E> : PropertyDrawer where E : Enum
    {
        private static readonly float headerPadding = 4f;
        private static readonly float itemsPadding = 4f;
        private static readonly float footerPadding = 6f;

        private static readonly RectOffset boxPadding = EditorStyles.helpBox.padding;
        private static readonly float lineHeight = EditorGUIUtility.singleLineHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {
                SerializedProperty enumArray = property.FindPropertyRelative("_enumArray");

                height += headerPadding;
                height += boxPadding.vertical;

                for (int i = 0; i < enumArray.arraySize; i++)
                {
                    if (i < enumArray.arraySize - 1)
                    {
                        height += itemsPadding;
                    }

                    height += boxPadding.vertical;
                    height += EditorGUI.GetPropertyHeight(enumArray.GetArrayElementAtIndex(i), true);
                }

                height += footerPadding;
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                Rect foldoutHeaderPosition = position;
                foldoutHeaderPosition.height = lineHeight;
                property.isExpanded = EditorGUI.Foldout(foldoutHeaderPosition, property.isExpanded, label, true);

                if (property.isExpanded)
                {
                    // Update array size matching Enum size
                    SerializedProperty enumArray = property.FindPropertyRelative("_enumArray");

                    Array enumValues = EnumArray<E>.ArrayEnum;
                    UpdateEnumArray(enumArray, enumValues);

                    position.height -= lineHeight + headerPadding + footerPadding;
                    position.y += lineHeight + headerPadding;
                    //GUI.Box(EditorGUI.IndentedRect(position), GUIContent.none, EditorStyles.helpBox);
                    //position = GUI.skin.box.padding.Remove(position);
                    //position.y += boxPadding.top;

                    EditorGUIVerticalLayout<EditorGUILayoutEnumElement> enumVerticalLayout = new EditorGUIVerticalLayout<EditorGUILayoutEnumElement>(position.x, position.y, boxPadding, itemsPadding);
                    {
                        //Add items
                        for (int i = 0; i < enumArray.arraySize; i++)
                        {
                            float propertyHeight = EditorGUI.GetPropertyHeight(enumArray.GetArrayElementAtIndex(i), true);
                            EditorGUILayoutEnumElement newEnumItem = new EditorGUILayoutEnumElement(enumArray.GetArrayElementAtIndex(i), ((E)enumValues.GetValue(i)).ToString());
                            enumVerticalLayout.AddElement(position.width, propertyHeight + boxPadding.vertical, newEnumItem);
                        }
                        //Draw
                        GUI.Box(EditorGUI.IndentedRect(enumVerticalLayout.Rect), GUIContent.none, EditorStyles.helpBox);
                        enumVerticalLayout.DrawElements();
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        protected bool UpdateEnumArray(SerializedProperty enumArray, Array enumValues)
        {
            if (enumArray.arraySize != enumValues.Length)
            {
                enumArray.arraySize = enumValues.Length;

                return true;
            }

            return false;
        }
    }
}