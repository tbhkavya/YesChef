using UnityEngine;

public class ProximityInteractor : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        if (currentInteractable != null &&
            Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}