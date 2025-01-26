using System.Collections;
using UnityEngine;

public class Karcher : Weapon {
    [SerializeField] private KarcherDetector detector;

    public async override void Attack() {
        StartCoroutine(AttackSequence());

        base.Attack();
    }

    private IEnumerator AttackSequence() {
        GarbageManager.Instance.FreePickups = true;
        detector.gameObject.SetActive(true);

        yield return new WaitForFixedUpdate();

        detector.gameObject.SetActive(false);
        GarbageManager.Instance.FreePickups = false;
    }
}
