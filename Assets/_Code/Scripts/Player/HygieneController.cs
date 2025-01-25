using UnityEngine;
using UnityEngine.UI;

public class HygieneController : MonoBehaviour {
    [SerializeField] private Image hygieneBar;
    [SerializeField] private int maxHygiene = 100;

    private int currentHygiene;

    private void Awake() {
        currentHygiene = maxHygiene;
    }

    void Start() {
        MessageBus.Instance.Subscribe("AddHygiene", AddHygiene);
        MessageBus.Instance.Subscribe("ReduceHygiene", ReduceHygiene);
    }

    private void AddHygiene(object value) {
        currentHygiene += (int)value;
        UpdateBar();
    }

    private void ReduceHygiene(object value) {
        currentHygiene -= (int)value;
        UpdateBar();
    }

    private void UpdateBar() {
        hygieneBar.fillAmount = 1f * currentHygiene / maxHygiene;
    }
}
