using UnityEngine;

public class Puddle : Dirt {
    [System.Serializable]
    public struct PuddleData {
        public int hygieneValue;
        public int maxHits;
    }

    [Header("Hygiene Values")]
    [SerializeField] private PuddleData[] puddleDataPerTier;

    private int remainingHits;

    private void Start() {
        if (puddleDataPerTier.Length != modelParent.childCount) {
            throw new UnityException("Incoherence in tier numbers and models in Puddle " + name);
        }
    }

    public override void CancelInteract() { }

    public override bool CanInteract() {
        return active;
    }

    public override void Interact() {
        if (CanDisappear()) {
            remainingHits--;
            weaponHandler.Current.IncreaseDirtLevel();

            if (remainingHits == 0) {
                Disappear();
            } else {
                modelParent.GetChild(tier).localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1f * remainingHits / puddleDataPerTier[tier].maxHits);
            }
        } else {
            animator.SetTrigger("WrongWeapon");
        }
    }

    public override void Appear() {
        base.Appear();

        remainingHits = puddleDataPerTier[tier].maxHits;
        modelParent.GetChild(tier).localScale = Vector3.one;
    }

    protected override void AddHygiene() {
        MessageBus.Instance.Notify("AddHygiene", puddleDataPerTier[tier].hygieneValue);
    }

    protected override void ReduceHygiene() {
        MessageBus.Instance.Notify("ReduceHygiene", puddleDataPerTier[tier].hygieneValue);
    }

    private void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1, 1, 0, .6f);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }

    protected override bool CanDisappear() {
        return weaponHandler.Current.Model == requiredWeapon && !weaponHandler.Current.IsFullDirty;
    }
}
