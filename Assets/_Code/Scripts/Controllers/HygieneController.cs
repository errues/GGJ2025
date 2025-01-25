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
        currentHygiene = Mathf.Clamp(currentHygiene + (int)value, 0, maxHygiene);
        UpdateBar();
    }

    private void ReduceHygiene(object value) {
        currentHygiene = Mathf.Clamp(currentHygiene - (int)value, 0, maxHygiene);
        UpdateBar();

        if (currentHygiene == 0) {
            MessageBus.Instance.Notify("GameOver");
        }
    }

    private void UpdateBar() {
        hygieneBar.fillAmount = 1f * currentHygiene / maxHygiene;
    }
}
