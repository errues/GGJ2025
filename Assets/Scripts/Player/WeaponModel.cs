using UnityEngine;

[CreateAssetMenu(fileName = "WeaponModel", menuName = "ScriptableObjects/WeaponModel", order = 1)]
public class WeaponModel : ScriptableObject
{
    public string Name;

    public GameObject Prefab;
    public Sprite PreviewSprite;

    //public float CooldownUseRateInSeconds; // secs between uses
    public float UsabilityTimeInSeconds; // total secs of usability before needing to recharge
    public float RechargingTimeInSeconds; // recharging time
    [Space]
    public string AnimationTrigger;
}



//public class PickeableWeapon : IInteractable
//{
//    public bool CanInteract()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void Interact()
//    {
//        throw new System.NotImplementedException();
//    }
//}