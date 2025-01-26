using System;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponModel", menuName = "ScriptableObjects/WeaponModel", order = 1)]
public class WeaponModel : ScriptableObject, IEquatable<WeaponModel>
{
    public string Name;
    public Sprite PreviewSprite;
    public int MaxDirtLevel = 6;

    public override bool Equals(object obj)
    {
        return Equals(obj as WeaponModel);
    }

    public bool Equals(WeaponModel other)
    {
        return other != null &&
               base.Equals(other) &&
               Name == other.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Name);
    }
}

