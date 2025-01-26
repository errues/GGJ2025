using UnityEngine;

public abstract class FixInteractable : MonoBehaviour, IInteractable {
    [SerializeField] protected WeaponModel requiredWeapon;

    protected CharacterWeaponHandler weaponHandler;

    protected abstract void InteractionAction();

    private void Awake() {
        weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
    }

    public void CancelInteract() { }

    public bool CanInteract() {
        return true;
    }

    public void Interact() {
        if (weaponHandler.Current.Model == requiredWeapon) {
            InteractionAction();
        }
    }
}
