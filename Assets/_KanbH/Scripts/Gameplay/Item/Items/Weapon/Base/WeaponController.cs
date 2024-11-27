using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public abstract void UseWeapon(Vector2 target, Vector2 self);
}
