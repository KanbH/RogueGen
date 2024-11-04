using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    private Item itemData;  // Reference to the item this dropped object represents

    public void Initialize(Item item)
    {
        itemData = item;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.Icon;
    }

    public void Interact(InventoryManager inventoryManager)
    {
        Debug.Log("Picked up " + itemData.ItemName);

        if (inventoryManager.PickUpItem(itemData))
        {
            DestroyItem();
        }
    }

    // Returns the item and destroys the GameObject when picked up
    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
