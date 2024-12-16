using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected EntityController _weaponUserController;
    public void SetWeaponUser(EntityController weaponUserController)
    {
        _weaponUserController = weaponUserController;
    }
    public abstract void UseWeapon(Vector2 target, Vector2 self);
}
