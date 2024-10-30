using UnityEngine;
using CharlieMadeAThing.NeatoTags.Core;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2.0f;  // Range within which interactions are allowed
    [SerializeField] private LayerMask interactableLayer;    // Layer for interactable objects, to optimize range checks

    private IInteractable currentInteractable;

    private void Update()
    {
        FindInteractableInRange();

        // Trigger interaction if within range and player presses "E"
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inventoryManager = GetComponent<InventoryManager>();
            currentInteractable.Interact(inventoryManager);
        }
    }

    private void FindInteractableInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        currentInteractable = null;  // Reset current interactable

        foreach (Collider2D hit in hits)
        {
            // Use HasTag for interactable tagging and check if object has IInteractable component
            if (hit.gameObject.HasTag("Interactable") && hit.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                currentInteractable = interactable;
                break;  // Stop at the first valid interactable
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw interaction range for visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
