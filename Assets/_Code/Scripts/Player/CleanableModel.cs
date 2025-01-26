using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CleanableModel", menuName = "ScriptableObjects/CleanableModel", order = 1)]
public class CleanableModel : ScriptableObject, IEquatable<CleanableModel> {
    public string Name;
    public Vector2 RandomDirtTimeLimits;
    public int MaxDirtLevel = 2;

    public override bool Equals(object obj) {
        return Equals(obj as CleanableModel);
    }

    public bool Equals(CleanableModel other) {
        return other != null &&
               base.Equals(other) &&
               Name == other.Name;
    }

    public override int GetHashCode() {
        return HashCode.Combine(base.GetHashCode(), Name);
    }
}
