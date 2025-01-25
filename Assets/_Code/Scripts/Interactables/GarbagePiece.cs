using UnityEngine;

public class GarbagePiece : MonoBehaviour, IInteractable {
    [SerializeField] private Transform modelParent;
    [SerializeField] private int hygieneValue = 1;

    [Header("Sounds")]
    [SerializeField] private ScriptableSound appearSound;
    [SerializeField] private ScriptableSound cleanSound;

    private Animator animator;
    private GarbageGenerator garbageGenerator;
    private AudioSource audioSource;

    private int randomModelIndex;
    private bool active;

    private void Awake() {
        animator = GetComponent<Animator>();
        garbageGenerator = GetComponentInParent<GarbageGenerator>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(appearSound.GetRandomClip());
        MessageBus.Instance.Notify("ReduceHygiene", hygieneValue);
    }

    public void Disappear() {
        active = false;
        modelParent.GetChild(randomModelIndex).gameObject.SetActive(false);
        garbageGenerator.SetGarbagePieceInactive(this);
        audioSource.PlayOneShot(cleanSound.GetRandomClip());
        MessageBus.Instance.Notify("AddHygiene", hygieneValue);
    }

    private void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1, 0, 0, .6f);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }
}
