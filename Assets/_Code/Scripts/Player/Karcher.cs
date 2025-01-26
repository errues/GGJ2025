using UnityEngine;

public class Karcher : Weapon {
    [SerializeField] private KarcherDetector detector;
    [SerializeField] private GameObject vfx;

    public async override void Attack() {

    }

    public void ActivateKarcher() {
        GarbageManager.Instance.FreePickups = true;
        detector.gameObject.SetActive(true);
        vfx.SetActive(true);
    }

    public void DeactivateKarcher() {
        detector.gameObject.SetActive(false);
        GarbageManager.Instance.FreePickups = false;
        vfx.SetActive(false);
    }
}
