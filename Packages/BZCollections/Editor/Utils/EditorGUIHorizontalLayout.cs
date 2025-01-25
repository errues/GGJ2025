using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BZ.Core.Collections.Editor
{
    public class EditorGUIHorizontalLayout<T> : BaseEditorGUILayout<T> where T : EditorGUILayoutElement
    {
        public EditorGUIHorizontalLayout(Vector2 position, float spacing = 0) : base(position)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }
        public EditorGUIHorizontalLayout(float x, float y, float spacing = 0) : base(x, y)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public EditorGUIHorizontalLayout(Vector2 position, RectOffset padding, float spacing = 0) : base(position, padding)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public EditorGUIHorizontalLayout(float x, float y, RectOffset padding, float spacing = 0) : base(x, y, padding)
        {
            Spacing = spacing;
            _elements = new List<T>();
        }

        public float Spacing { get; set; }

        public override T AddElement(float width, float height, T newElement)
        {
            float x = _initialPosition.x + Padding.left + _elements.Sum(element => element.Rect.width) + TotalSpacing + Spacing;
            float y = _initialPosition.y + Padding.top;

            newElement.Rect = new Rect(x, y, width, height);
            _elements.Add(newElement);

            return newElement;
        }

        public override float GetHeight()
        {
            return Padding.top + _elements.Max(element => element.Rect.height);
        }

        public override float GetWidth()
        {
            return Padding.left + Padding.right + _elements.Sum(element => element.Rect.width) + TotalSpacing + Padding.right;
        }

        private float TotalSpacing => Spacing * (_elements.Count - 1);
    }
}