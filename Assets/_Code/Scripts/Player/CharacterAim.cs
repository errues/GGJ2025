using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour {
    [SerializeField] private Vector2 mouseAimSensitivity;
    [SerializeField] private Vector2 gamepadAimSensitivity;
    [SerializeField] private Transform cameraTransform;

    private Vector2 aimInput;
    private float xAngle;

    private Rigidbody rb;
    private PlayerInput playerInput;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked; // Provisional

        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        bool gamepad = playerInput.currentControlScheme == "Gamepad";
        rb.MoveRotation(Quaternion.Euler(0, rb.rotation.eulerAngles.y + (aimInput.x * (gamepad ? gamepadAimSensitivity.x * Time.deltaTime : mouseAimSensitivity.x)), 0));
        xAngle = Mathf.Clamp(xAngle - aimInput.y * (gamepad ? gamepadAimSensitivity.y * Time.deltaTime : mouseAimSensitivity.y), -90, 90);
        cameraTransform.localEulerAngles = new Vector3(xAngle, 0, 0);
    }

    private void OnLook(InputValue value) {
        aimInput = value.Get<Vector2>();
    }
}
