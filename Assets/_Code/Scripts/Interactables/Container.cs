using System.Collections;
using UnityEngine;

public class Container : FixInteractable {
    [SerializeField] private GameObject garbageBin;

    private Coroutine garbageCoroutine;


    protected override void InteractionAction() {
        if (GarbageManager.Instance.CurrentGarbageAmount > 0) {
            GarbageManager.Instance.EmptyGarbage();

            if (garbageCoroutine != null) {
                StopCoroutine(garbageCoroutine);
            }
            garbageCoroutine = StartCoroutine(GarbageSequence());
        }
    }

    private IEnumerator GarbageSequence() {
        garbageBin.SetActive(true);

        yield return new WaitForSeconds(3);

        garbageBin.SetActive(false);
    }
}
