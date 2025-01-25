using UnityEngine;

public class RotationFollower : MonoBehaviour {
    [SerializeField] private Transform rotationTarget;
    [SerializeField] private float followingSpeed;

    void Update() {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget.rotation, followingSpeed * Time.deltaTime);
    }
}
