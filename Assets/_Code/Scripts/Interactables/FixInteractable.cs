using UnityEngine;

public abstract class FixInteractable : MonoBehaviour, IInteractable {
    [SerializeField] protected WeaponModel requiredWeapon;

    protected CharacterWeaponHandler weaponHandler;
    private OutlineRenderer outlineRenderer;

    protected abstract void InteractionAction();

    private void Awake() {
        weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
        outlineRenderer = GetComponentInChildren<OutlineRenderer>();
    }

    public void CancelInteract() {
        outlineRenderer.enabled = false;
    }

    public void EnteredInteractionRange() {
        outlineRenderer.enabled = true;
    }

    public bool CanInteract() {
        return true;
    }

    public void Interact() {
        if (weaponHandler.Current.Model == requiredWeapon) {
            InteractionAction();
        }
    }
}
