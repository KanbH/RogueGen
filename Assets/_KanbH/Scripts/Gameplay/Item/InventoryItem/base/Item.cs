using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/TestItem")]
public class Item : ScriptableObject
{
    [SerializeField] public string ItemName;
    [SerializeField] public string Description;
    [SerializeField] public Sprite Icon;

    [SerializeField] private GameObject DroppedItemPrefab;

    public void DropItem(Vector2 dropPosition)
    {
        if (DroppedItemPrefab == null)
        {
            Debug.LogWarning("DroppedItemPrefab is not assigned!");
            return;
        }

        // Instantiate the dropped item in the world
        GameObject droppedItemObj = Instantiate(DroppedItemPrefab, dropPosition, Quaternion.identity);

        // Initialize the DroppedItem component with the item data
        DroppedItem droppedItem = droppedItemObj.GetComponent<DroppedItem>();
        if (droppedItem != null)
        {
            droppedItem.Initialize(this);  // Pass the item data to the dropped item
        }
        else
        {
            Debug.LogError("DroppedItemPrefab does not contain a DroppedItem component!");
        }
    }
}
