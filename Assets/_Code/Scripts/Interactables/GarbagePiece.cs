using UnityEngine;

public class GarbagePiece : Dirt {
    [Header("Hygiene Values")]
    [SerializeField] private int hygieneValue = 1;

    public override void CancelInteract() { }

    public override bool CanInteract() {
        return active;
    }

    protected override void AddHygiene() {
        MessageBus.Instance.Notify("AddHygiene", hygieneValue);
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
