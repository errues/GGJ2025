using UnityEngine;

public class Container : MonoBehaviour, IInteractable {
    [SerializeField] protected WeaponModel requiredWeapon;

    protected CharacterWeaponHandler weaponHandler;

    private void Awake() {
        weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
    }

    public void CancelInteract() { }

    public bool CanInteract() {
        return true;
    }

    public void Interact() {
        if (weaponHandler.Current.Model == requiredWeapon) {
            GarbageManager.Instance.EmptyGarbage();
        }
    }

}
