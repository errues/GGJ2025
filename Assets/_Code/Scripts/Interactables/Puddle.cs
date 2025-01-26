using UnityEngine;

public class Puddle : Dirt {
    [Header("Hygiene Values")]
    [SerializeField] private int[] hygieneValuesPerTier;

    private void Start() {
        if (hygieneValuesPerTier.Length != modelParent.childCount) {
            throw new UnityException("Incoherence in tier numbers and models in Puddle " + name);
        }
    }

    public override void CancelInteract() { }

    public override bool CanInteract() {
        return active; // Dependerá del arma
    }

    protected override void AddHygiene() {
        MessageBus.Instance.Notify("AddHygiene", hygieneValuesPerTier[tier]);
    }

    protected override void ReduceHygiene() {
        MessageBus.Instance.Notify("ReduceHygiene", hygieneValuesPerTier[tier]);
    }

    private void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1, 1, 0, .6f);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }

    protected override bool CanDisappear() {
        return weaponHandler.Current.Model == requiredWeapon; // Falta comprobar que no esté sucia
    }
}
