using System;
using UnityEngine;
 

[CreateAssetMenu(fileName = "WeaponModel", menuName = "ScriptableObjects/WeaponModel", order = 1)]
public class WeaponModel : ScriptableObject, IEquatable<WeaponModel>
{
    public string Name;

    public GameObject Prefab;
    public Sprite PreviewSprite;

    //public float CooldownUseRateInSeconds; // secs between uses
    public float UsabilityTimeInSeconds; // total secs of usability before needing to recharge
    public float RechargingTimeInSeconds; // recharging time
    [Space]
    public string AnimationTrigger;

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