using UnityEngine;
using NUnit.Framework;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();

        Assert.IsNotNull(_inventory);
    }

    public bool PickUpItem(Item item)
    {
        return _inventory.AddItem(item);
    }

}
