using UnityEngine;
using UnityEngine.UI;

public class GarbageManager : MonoBehaviour {
    [SerializeField] private int garbageCapacity;
    [SerializeField] private Image garbageBar;

    private int currentGarbageAmount;

    public static GarbageManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void PickUpGarbage() {
        currentGarbageAmount++;
        UpdateBar();
    }

    public void EmptyGarbage() {
        currentGarbageAmount = 0;
        UpdateBar();
    }

    public bool CanSpaceGarbage() {
        return currentGarbageAmount < garbageCapacity;
    }

    private void UpdateBar() {
        garbageBar.fillAmount = 1f * currentGarbageAmount / garbageCapacity;
    }
}
