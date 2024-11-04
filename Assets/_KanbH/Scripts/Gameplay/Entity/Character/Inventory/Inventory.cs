using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> InventoryItems = new List<Item>();
    [SerializeField] private int _maxCapacity;

    public bool AddItem(Item item)
    {
        if (InventoryItems.Count >= _maxCapacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        InventoryItems.Add(item);
        Debug.Log($"Added {item.ItemName} to inventory.");
        return true;
    }

    public void RemoveItem(Item item)
    {
        InventoryItems.Remove(item);
        Debug.Log($"Removed {item.ItemName} from inventory.");
    }

    public bool ContainsItem(Item item)
    {
        return InventoryItems.Contains(item);
    }

    public Item FindFirstItem()
    {
        return InventoryItems[0];
    }

    public Weapon FindFirstWeapon()
    {
        foreach (Item item in InventoryItems)
        {
            if (item is Weapon weapon)
            {
                return weapon;
            }
        }
        return null;
    }

    public void RemoveAndDropItem(Item item)
    {
        RemoveItem(item);
        item.DropItem(this.transform.position);
    }
}
