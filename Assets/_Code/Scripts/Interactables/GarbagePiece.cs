using UnityEngine;

public class GarbagePiece : MonoBehaviour, IInteractable {
    [SerializeField] private Transform modelParent;

    private Animator animator;
    private GarbageGenerator garbageGenerator;

    private int randomModelIndex;
    private bool active;

    private void Awake() {
        animator = GetComponent<Animator>();
        garbageGenerator = GetComponentInParent<GarbageGenerator>();
    }

    public void CancelInteract() { }

    public bool CanInteract() {
        return active;
    }

    public void Interact() {
        Disappear();
    }

    public void Appear() {
        active = true;
        randomModelIndex = Random.Range(0, modelParent.childCount);
        modelParent.GetChild(randomModelIndex).gameObject.SetActive(true);
        animator.SetTrigger("Appear");
        modelParent.Rotate(0, Random.Range(0f, 360f), 0);
        // Sumar al medidor general de suciedad
    }

    public void Disappear() {
        active = false;
        modelParent.GetChild(randomModelIndex).gameObject.SetActive(false);
        garbageGenerator.SetGarbagePieceInactive(this);
        // Restar al medidor general de suciedad
    }

    private void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1, 0, 0, .6f);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }
}
