using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [SerializeField] public GameObject WeaponPrefab;

    private WeaponController _controllerInstance;

    public void UseWeapon(Vector2 attackDirection, Transform characterTransform)
    {
        if (_controllerInstance == null)
        {
            GameObject weaponInstance = Instantiate(WeaponPrefab, characterTransform);

            // Get the WeaponController script and start the swing
            _controllerInstance = weaponInstance.GetComponent<WeaponController>();
            _controllerInstance.StartSwing(attackDirection);
        }
    }
}
