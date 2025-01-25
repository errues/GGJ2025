using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirtGenerator : MonoBehaviour {
    [SerializeField] private Vector2 randomAppearTimeLimits;

    private List<Dirt> inactiveDirt;
    private List<Dirt> activeDirt;

    private float nextDirtAppearTime;

    private void Awake() {
        inactiveDirt = GetComponentsInChildren<Dirt>().ToList();
        activeDirt = new List<Dirt>();

        nextDirtAppearTime = Random.Range(randomAppearTimeLimits.x, randomAppearTimeLimits.y);
    }

    private void Update() {
        if (Time.time > nextDirtAppearTime) {
            nextDirtAppearTime = Time.time + Random.Range(randomAppearTimeLimits.x, randomAppearTimeLimits.y);
            ActivateRandomDirt();
        }
    }

    private void ActivateRandomDirt() {
        if (inactiveDirt.Count > 0) {
            Dirt dirt = inactiveDirt[Random.Range(0, inactiveDirt.Count)];
            inactiveDirt.Remove(dirt);
            activeDirt.Add(dirt);
            dirt.Appear();
        }
    }

    public void SetDirtInactive(Dirt piece) {
        if (activeDirt.Remove(piece)) {
            inactiveDirt.Add(piece);
        }
    }
}
