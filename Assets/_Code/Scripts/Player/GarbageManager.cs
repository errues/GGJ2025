using UnityEngine;
using UnityEngine.UI;

public class GarbageManager : MonoBehaviour {
    [SerializeField] private int garbageCapacity;
    [SerializeField] private Image garbageBar;

    [SerializeField] private GameObject garbageModelTier1;
    [SerializeField] private GameObject garbageModelTier2;
    [SerializeField] private GameObject garbageModelTier3;

    private int currentGarbageAmount;

    public static GarbageManager Instance { get; private set; }
    public bool FreePickups { get; set; }

    private void Awake() {
        Instance = this;
    }

    public void PickUpGarbage() {
        if (!FreePickups) {
            currentGarbageAmount++;
            UpdateVisuals();
        }
    }

    public void EmptyGarbage() {
        currentGarbageAmount = 0;
        UpdateVisuals();
    }

    public bool CanSpaceGarbage() {
        return currentGarbageAmount < garbageCapacity;
    }

    private void UpdateVisuals() {
        garbageBar.fillAmount = 1f * currentGarbageAmount / garbageCapacity;

        garbageModelTier1.SetActive(currentGarbageAmount > 0);
        garbageModelTier2.SetActive(currentGarbageAmount >= garbageCapacity / 2);
        garbageModelTier3.SetActive(currentGarbageAmount == garbageCapacity);
    }
}
