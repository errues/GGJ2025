using UnityEngine;

public class KarcherDetector : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Dirt dirt)) {
            dirt.Disappear();
        } else if (other.TryGetComponent(out Jammer jammer)) {
            jammer.ReduceDirtLevel();
            jammer.ReduceDirtLevel();
        }
    }
}
