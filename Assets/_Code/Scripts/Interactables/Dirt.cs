using UnityEngine;

public abstract class Dirt : MonoBehaviour, IInteractable {
    [SerializeField] protected Transform modelParent;

    [Header("Sounds")]
    [SerializeField] protected ScriptableSound appearSound;
    [SerializeField] protected ScriptableSound cleanSound;

    protected bool active;
    protected int tier;

    protected Animator animator;
    protected DirtGenerator garbageGenerator;
    protected AudioSource audioSource;

    private void Awake() {
        animator = GetComponent<Animator>();
        garbageGenerator = GetComponentInParent<DirtGenerator>();
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void CancelInteract();
    public abstract bool CanInteract();
    protected abstract void ReduceHygiene();
    protected abstract void AddHygiene();

    public void Interact() {
        Disappear();
    }

    public void Appear() {
        active = true;
        tier = Random.Range(0, modelParent.childCount);
        modelParent.GetChild(tier).gameObject.SetActive(true);
        animator.SetTrigger("Appear");
        modelParent.Rotate(0, Random.Range(0f, 360f), 0);

        if (appearSound != null) {
            audioSource.PlayOneShot(appearSound.GetRandomClip());
        }

        ReduceHygiene();
    }

    public void Disappear() {
        active = false;
        modelParent.GetChild(tier).gameObject.SetActive(false);
        garbageGenerator.SetDirtInactive(this);

        if (cleanSound != null) {
            audioSource.PlayOneShot(cleanSound.GetRandomClip());
        }

        AddHygiene();
    }
}
