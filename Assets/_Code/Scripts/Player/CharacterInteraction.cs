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
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }


        bool showSPrite = currentInteractable != null && currentInteractable.CanInteract();
        interactionSprite.gameObject.SetActive(showSPrite);
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




//public class PickeableWeapon : IInteractable
//{
//    public bool CanInteract()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void Interact()
//    {
//        throw new System.NotImplementedException();
//    }
//}