using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Weapon _equippedWeapon;

    private GameObject _weaponInstance;
    private WeaponController _weaponController;

    private void Awake()
    {
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
        if (_equippedWeapon.WeaponPrefab != null)
        {
            _weaponInstance = Instantiate(_equippedWeapon.WeaponPrefab, transform);
            _weaponController = _weaponInstance.GetComponent<WeaponController>();
            //_weaponInstance.SetActive(false);
        }
    }

    // Perform an attack
    public void PerformAttack(Vector2 attackDirection)
    {
        if (_weaponInstance != null)
        {
            _weaponInstance.SetActive(true);
            // Attack logic can vary, here�fs a simple example:
            _weaponController.UseWeapon(attackDirection);
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