using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour {
    private const float RealtimeDuration = 172800;

    [SerializeField, Range(10, 600)] private float gameDuration = 300;
    [SerializeField] private TextMeshProUGUI text;

    private float remainingTime;
    private bool timerActive = true;

    private void Awake() {
        remainingTime = gameDuration;
    }

    void Update() {
        if (timerActive) {
            remainingTime = Mathf.Clamp(remainingTime - Time.deltaTime, 0, gameDuration);

            UpdateTimerText();

            if (remainingTime <= 0) {
                MessageBus.Instance.Notify("TimeUp");
                timerActive = false;
            }
        }
    }

    private void UpdateTimerText() {
        float hypotheticalTime = remainingTime * (RealtimeDuration / gameDuration);
        int hours = (int)hypotheticalTime / 3600;
        int minutes = ((int)hypotheticalTime - hours * 3600) / 60;
        text.text = (hours < 10 ? "0" : "") + hours + ":" + (minutes < 10 ? "0" : "") + minutes;
    }
}
