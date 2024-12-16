using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Weapon _equippedWeapon;

    private EntityController _entityController;
    private GameObject _weaponInstance;
    private WeaponController _weaponController;

    private void Awake()
    {
        _entityController = GetComponent<EntityController>();
        InstantiateEquippedWeapon();
    }

    public void EquipWeapon(Weapon weapon)
    {
        _equippedWeapon = weapon;
        InstantiateEquippedWeapon();
    }

    private void InstantiateEquippedWeapon()
    {
        // Destroy previous weapon instance if it exists
        if (_weaponInstance != null)
        {
            Destroy(_weaponInstance);
        }

        // Instantiate the weapon prefab as a child of the player (optional, based on your setup)
        if (_equippedWeapon != null && _equippedWeapon.WeaponPrefab != null)
        {
            _weaponInstance = Instantiate(_equippedWeapon.WeaponPrefab, transform);
            _weaponController = _weaponInstance.GetComponent<WeaponController>();
            _weaponController.SetWeaponUser(_entityController);
            _weaponInstance.SetActive(false);
        }
    }

    // Perform an attack
    public void PerformAttack(Vector2 attackDirection, Vector2 attackerPosition)
    {
        if (_weaponInstance != null)
        {
            _weaponInstance.SetActive(true);
            // Attack logic can vary, hereÅfs a simple example:
            _weaponController.UseWeapon(attackDirection, attackerPosition);
        }
    }
    
    public void UnequipWeapon()
    {
        if (_weaponInstance != null)
        {
            Destroy(_weaponInstance);
        }
        _equippedWeapon = null;
    }
    
}
