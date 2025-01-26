using UnityEngine;
using UnityEngine.UI;

public class GarbageManager : MonoBehaviour {
    [SerializeField] private int garbageCapacity;
    [SerializeField] private Image garbageBar;

    [SerializeField] private GameObject garbageModelTier1;
    [SerializeField] private GameObject garbageModelTier2;
    [SerializeField] private GameObject garbageModelTier3;

    public int CurrentGarbageAmount { get; private set; }

    public static GarbageManager Instance { get; private set; }
    public bool FreePickups { get; set; }

    private void Awake() {
        Instance = this;
    }

    public void PickUpGarbage() {
        if (!FreePickups) {
            CurrentGarbageAmount++;
            UpdateVisuals();
        }
    }

    public void EmptyGarbage() {
        CurrentGarbageAmount = 0;
        UpdateVisuals();
    }

    public bool CanSpaceGarbage() {
        return CurrentGarbageAmount < garbageCapacity;
    }

    private void UpdateVisuals() {
        garbageBar.fillAmount = 1f * CurrentGarbageAmount / garbageCapacity;

        garbageModelTier1.SetActive(CurrentGarbageAmount > 0);
        garbageModelTier2.SetActive(CurrentGarbageAmount >= garbageCapacity / 2);
        garbageModelTier3.SetActive(CurrentGarbageAmount == garbageCapacity);
    }
}
