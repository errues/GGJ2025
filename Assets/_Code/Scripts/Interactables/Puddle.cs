using UnityEngine;
using System.Collections;


public class Puddle : Dirt {
    [System.Serializable]
    public struct PuddleData {
        public int hygieneValue;
        public int maxHits;
    }

    [Header("Hygiene Values")]
    [SerializeField] private PuddleData[] puddleDataPerTier;

    [SerializeField] private ParticleSystem particleFX;
    private int remainingHits;

    private void Start() {
        if (puddleDataPerTier.Length != modelParent.childCount) {
            throw new UnityException("Incoherence in tier numbers and models in Puddle " + name);
        }
    }

    public override void CancelInteract() { }
    public override void EnteredInteractionRange() { }

    public override bool CanInteract() {
        return active;
    }

    public override void Interact()
    {
        if (CanDisappear())
        {
            remainingHits--;
            weaponHandler.Current.IncreaseDirtLevel();
            PlayParticles();
            if (remainingHits == 0)
            {
                Disappear();
            }
            else
            {
                float targetScale = 1f * remainingHits / puddleDataPerTier[tier].maxHits;
                StartCoroutine(AnimateScale(modelParent.GetChild(tier), targetScale));
            }
        }
        else
        {
            animator.SetTrigger("WrongWeapon");
        }
    }

    private IEnumerator AnimateScale(Transform target, float targetScale)
    {
        Vector3 startScale = target.localScale; // Escala inicial
        Vector3 endScale = Vector3.one * targetScale; // Escala final basada en el progreso
        float duration = 0.5f; // Duración de la animación
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Progreso normalizado [0, 1]
            target.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null; // Esperar al siguiente frame
        }

        target.localScale = endScale; // Asegurar la escala final exacta
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

    public void PlayParticles()
    {
        if (particleFX != null)
        {
            particleFX.Play(); // Reproduce el sistema de partículas
        }
        else
        {
            Debug.LogWarning("No ParticleSystem assigned!");
        }
    }
}
