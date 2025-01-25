using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace BZ.Core.Collections.Editor
{
    public class EditorGUIVerticalLayout<T> : BaseEditorGUILayout<T> where T : EditorGUILayoutElement
    {
        public EditorGUIVerticalLayout(Vector2 position, float spacing = 0) : base(position)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }
        public EditorGUIVerticalLayout(float x, float y, float spacing = 0) : base(x, y)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public EditorGUIVerticalLayout(Vector2 position, RectOffset padding, float spacing = 0) : base(position, padding)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public EditorGUIVerticalLayout(float x, float y, RectOffset padding, float spacing = 0) : base(x, y, padding)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public float Spacing { get; set; }

        public override T AddElement(float width, float height, T newElement)
        {
            float x = _initialPosition.x + Padding.left;
            float y = _initialPosition.y + Padding.top + _elements.Sum(element => element.Rect.height) + TotalSpacing + Spacing;

            newElement.Rect = new Rect(x, y, width, height);
            _elements.Add(newElement);

            return newElement;
        }

        public override float GetHeight()
        {
            return Padding.top + _elements.Sum(element => element.Rect.height) + TotalSpacing + Padding.bottom;
        }

        public override float GetWidth()
        {
            if (_elements.Count <= 0) return 0;
            return Padding.left + Padding.right + _elements.Max(element => element.Rect.width);
        }

        private float TotalSpacing => Spacing * (_elements.Count - 1);
    }
}