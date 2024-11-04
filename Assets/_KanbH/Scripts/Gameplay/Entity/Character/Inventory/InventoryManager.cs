using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Weapon EquippedWeapon;

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

    public void AttemptToUseWeapon(Vector2 attackDirection)
    {
        EquippedWeapon.UseWeapon(attackDirection, this.transform);
    }

}
