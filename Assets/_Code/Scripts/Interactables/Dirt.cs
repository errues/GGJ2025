using UnityEngine;
using System.Collections;

public abstract class Dirt : MonoBehaviour, IInteractable {
    [SerializeField] protected Transform modelParent;
    [SerializeField] protected WeaponModel requiredWeapon;

    [Header("Sounds")]
    [SerializeField] protected ScriptableSound appearSound;
    [SerializeField] protected ScriptableSound cleanSound;

    protected bool active;
    protected int tier;

    protected Animator animator;
    protected DirtGenerator garbageGenerator;
    protected AudioSource audioSource;
    protected CharacterWeaponHandler weaponHandler;

    private void Awake() {
        animator = GetComponent<Animator>();
        garbageGenerator = GetComponentInParent<DirtGenerator>();
        audioSource = GetComponent<AudioSource>();
        weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
    }

    public abstract void CancelInteract();
    public abstract void EnteredInteractionRange();
    public abstract bool CanInteract();
    protected abstract void ReduceHygiene();
    protected abstract void AddHygiene();
    protected abstract bool CanDisappear();
    public abstract void Interact();

    protected virtual void Disappear()
    {
        active = false;

        // Inicia la animación de desaparición progresiva
        StartCoroutine(AnimateDisappear(modelParent.GetChild(tier)));

        // Desactiva la suciedad del generador
        garbageGenerator.SetDirtInactive(this);

        if (cleanSound != null)
        {
            audioSource.PlayOneShot(cleanSound.GetRandomClip());
        }

        AddHygiene();
    }

    // Corutina para animar la desaparición del objeto
    private IEnumerator AnimateDisappear(Transform target)
    {
        Vector3 startScale = target.localScale; // Escala inicial
        Vector3 endScale = Vector3.zero; // Escala final (desaparece completamente)
        float duration = 0.5f; // Duración de la animación
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Progreso normalizado [0, 1]
            target.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null; // Esperar al siguiente frame
        }

        target.localScale = endScale; // Asegurar que la escala final sea cero
        target.gameObject.SetActive(false); // Finalmente desactivar el objeto
    }

    public virtual void Appear() {
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
}
