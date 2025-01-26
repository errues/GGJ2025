using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour {
    private const float RealtimeDuration = 172800;

    [SerializeField, Range(10, 600)] private float gameDuration = 300;
    [SerializeField] private TextMeshProUGUI text;

    private float remainingTime;
    private bool timerActive = true;

    public float HypotheticalTime { get; private set; }

    private void Awake() {
        remainingTime = gameDuration;
        MessageBus.Instance.Subscribe("TimeUp", TimeUp);
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
        HypotheticalTime = remainingTime * (RealtimeDuration / gameDuration);
        int hours = (int)HypotheticalTime / 3600;
        int minutes = ((int)HypotheticalTime - hours * 3600) / 60;
        text.text = (hours < 10 ? "0" : "") + hours + ":" + (minutes < 10 ? "0" : "") + minutes;
    }

    private void TimeUp(object value)
    {
        SceneManager.LoadScene("WinScene");
    }
}
