using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Image interactionSprite;

    private IInteractable currentInteractable;

    void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, raycastDistance))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
            }
            else
            {
                if (currentInteractable != null)
                    currentInteractable.CancelInteract();
                currentInteractable = null;
            }
        }
        else
        {
            if (currentInteractable != null)
                currentInteractable.CancelInteract();
            currentInteractable = null;
        }


        bool showSprite = currentInteractable != null && currentInteractable.CanInteract();
        interactionSprite.color = showSprite? Color.red : Color.white;
    }

    private void OnInteract()
    {
        if (currentInteractable != null && currentInteractable.CanInteract())
        {
            currentInteractable.Interact();
        }
    }

    private void OnAttack()
    {
        if (currentInteractable != null && currentInteractable.CanInteract())
        {
            currentInteractable.Interact();
        }
    }
}