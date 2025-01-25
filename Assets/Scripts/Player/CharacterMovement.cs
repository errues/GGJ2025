using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour {
    [SerializeField, Range(.5f, 15)] private float movementSpeed;
    [SerializeField, Range(5f, 80)] private float acceleration;
    [SerializeField, Range(1, 3)] private float sprintSpeedMultiplier;

    private Rigidbody rb;
    private PlayerInput playerInput;

    private Vector2 moveInput;
    private Vector2 currentMovementVelocity;
    private bool sprintInput;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate() {
        Vector2 targetSpeed = moveInput * movementSpeed * (sprintInput ? sprintSpeedMultiplier : 1);
        currentMovementVelocity = Vector2.MoveTowards(currentMovementVelocity, targetSpeed, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = transform.rotation * new Vector3(currentMovementVelocity.x, rb.linearVelocity.y, currentMovementVelocity.y);
    }

    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    private void OnSprint(InputValue value) {
        sprintInput = value.isPressed;
    }
}
