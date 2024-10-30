using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _inventory.RemoveAndDropItem(_inventory.FindFirstItem());
        }
    }

    public bool PickUpItem(Item item)
    {
        return _inventory.AddItem(item);
    }

}
