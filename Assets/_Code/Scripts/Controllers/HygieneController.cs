using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HygieneController : MonoBehaviour {
    [SerializeField] private Image hygieneBar;
    [SerializeField] private int maxHygiene = 100;

    [Header("Post-Processing")]
    [SerializeField, Range(0, 1)] private float postprocessingChangeSpeed = .2f;
    [SerializeField] private Volume cleanVolume;
    [SerializeField] private Volume midVolume;
    [SerializeField] private Volume dirtyVolume;

    private int currentHygiene;
    private float cleanWeightTarget = 1;
    private float midWeightTarget;
    private float dirtyWeightTarget;

    private void Awake() {
        currentHygiene = maxHygiene;
    }

    void Start() {
        MessageBus.Instance.Subscribe("AddHygiene", AddHygiene);
        MessageBus.Instance.Subscribe("ReduceHygiene", ReduceHygiene);
        MessageBus.Instance.Subscribe("GameOver", GameOver);
    }

    private void Update() {
        cleanVolume.weight = Mathf.MoveTowards(cleanVolume.weight, cleanWeightTarget, postprocessingChangeSpeed * Time.deltaTime);
        midVolume.weight = Mathf.MoveTowards(midVolume.weight, midWeightTarget, postprocessingChangeSpeed * Time.deltaTime);
        dirtyVolume.weight = Mathf.MoveTowards(dirtyVolume.weight, dirtyWeightTarget, postprocessingChangeSpeed * Time.deltaTime);
    }

    private void AddHygiene(object value) {
        currentHygiene = Mathf.Clamp(currentHygiene + (int)value, 0, maxHygiene);
        UpdateVisuals();
    }

    private bool gameOver;
    private void ReduceHygiene(object value) {

        currentHygiene = Mathf.Clamp(currentHygiene - (int)value, 0, maxHygiene);
        UpdateVisuals();

        if (currentHygiene == 0) {
            MessageBus.Instance.Notify("GameOver");


        }
    }

    private void UpdateVisuals() {
        float normalizedHygiene = 1f * currentHygiene / maxHygiene;
        hygieneBar.fillAmount = normalizedHygiene;

        cleanWeightTarget = Mathf.Lerp(0, 1, (normalizedHygiene - .5f) / .33f);
        midWeightTarget = .5f - (Mathf.Abs(.5f - normalizedHygiene) - 0.166f) / .33f;
        dirtyWeightTarget = 1 - Mathf.Lerp(0, 1, (normalizedHygiene - .166f) / .33f);
    }

    private void GameOver(object value)
    {
        if(!gameOver)
        {
            gameOver = true;
            SceneManager.LoadScene("LooseScene");
        }
        
    }

}
