using UnityEngine;

namespace BZ.Core.Collections.Editor
{
    public abstract class EditorGUILayoutElement
    {
        public Rect Rect { get; set; }
        public abstract void Draw();
    }
}