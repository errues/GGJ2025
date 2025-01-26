using UnityEngine;

public class GarbagePiece : Dirt {
    [Header("Hygiene Values")]
    [SerializeField] private int hygieneValue = 1;

    protected OutlineRenderer modelOutline;

    public override void CancelInteract() {
        if (modelOutline)
            modelOutline.enabled = false;
    }

    public override void EnteredInteractionRange() {
        if (modelOutline)
            modelOutline.enabled = true;
    }

    public override bool CanInteract() {
        if (modelOutline)
            modelOutline.enabled = true;

        return active;
    }

    public override void Interact() {
        if (CanDisappear()) {
            Disappear();
        } else {
            animator.SetTrigger("WrongWeapon");
        }
    }

    protected override void AddHygiene() {
        MessageBus.Instance.Notify("AddHygiene", hygieneValue);
    }

    protected override bool CanDisappear() {
        return weaponHandler.Current.Model == requiredWeapon && GarbageManager.Instance.CanSpaceGarbage();
    }

    public override void Appear() {
        base.Appear();

        modelOutline = modelParent.GetChild(tier).gameObject.GetComponent<OutlineRenderer>();
    }

    protected override void Disappear() {
        if (modelOutline)
            modelOutline.enabled = false;
        modelOutline = null;

        base.Disappear();

        GarbageManager.Instance.PickUpGarbage();
    }

    protected override void ReduceHygiene() {
        MessageBus.Instance.Notify("ReduceHygiene", hygieneValue);
    }

    private void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1, 0, 0, .6f);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }
}
