using System.Collections.Generic;
using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public abstract class BaseEditorGUILayout<T> where T : EditorGUILayoutElement
    {
        public Rect Rect => new Rect(_initialPosition.x, _initialPosition.y, GetWidth(), GetHeight());

        protected Vector2 _initialPosition;

        protected List<T> _elements;
        public abstract float GetWidth();
        public abstract float GetHeight();

        public RectOffset Padding { get; set; }

        public BaseEditorGUILayout(Vector2 position)
        {
            _initialPosition = position;
            Padding = new RectOffset(0, 0, 0, 0);
        }

        public BaseEditorGUILayout(float x, float y)
        {
            _initialPosition = new Vector2(x, y);
            Padding = new RectOffset(0, 0, 0, 0);
        }

        public BaseEditorGUILayout(Vector2 position, RectOffset padding)
        {
            _initialPosition = position;
            Padding = padding;
        }

        public BaseEditorGUILayout(float x, float y, RectOffset padding)
        {
            _initialPosition = new Vector2(x, y);
            Padding = padding;
        }

        public void DrawElements() => _elements.ForEach(element => element.Draw());

        public abstract T AddElement(float width, float height, T newElement);
    }
}