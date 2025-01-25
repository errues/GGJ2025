using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField, Range(.5f, 15)] private float movementSpeed;
    [SerializeField, Range(5f, 80)] private float acceleration;
    [SerializeField, Range(1, 3)] private float sprintSpeedMultiplier;
    [SerializeField, Range(1f, 20f)] private float lerpSpeed; // Velocidad para interpolar el movimiento del Animator

    private Rigidbody rb;
    private PlayerInput playerInput;

    private Vector2 moveInput;
    private Vector2 currentMovementVelocity;
    private Vector2 interpolatedMovementVelocity; // Nuevo miembro para valores suavizados
    private bool sprintInput;

    [SerializeField] private CharacterWeaponHandler _weaponHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Gamepad" && moveInput == Vector2.zero)
        {
            sprintInput = false;
        }

        // Interpola el valor del Animator para transiciones m�s suaves
        interpolatedMovementVelocity = Vector2.Lerp(interpolatedMovementVelocity, currentMovementVelocity, lerpSpeed * Time.deltaTime);

        // Actualiza el par�metro del Animator con el valor suavizado
        _weaponHandler.CurrentWeaponAnimator.SetFloat("Speed", interpolatedMovementVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        // Calcula la velocidad objetivo basada en la entrada de movimiento
        Vector2 targetSpeed = moveInput * movementSpeed * (sprintInput ? sprintSpeedMultiplier : 1);

        // Cambia gradualmente la velocidad actual hacia la velocidad objetivo
        currentMovementVelocity = Vector2.MoveTowards(currentMovementVelocity, targetSpeed, acceleration * Time.fixedDeltaTime);

        // Aplica la velocidad al Rigidbody
        rb.linearVelocity = transform.rotation * new Vector3(currentMovementVelocity.x, rb.linearVelocity.y, currentMovementVelocity.y);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnSprint(InputValue value)
    {
        sprintInput = value.isPressed || playerInput.currentControlScheme == "Gamepad";
    }

    private void OnControlsChanged()
    {
        sprintInput = false;
    }
}
