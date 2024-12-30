using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBowController : WeaponController
{
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float _arrowSpeed;

    public override void UseWeapon(Vector2 attackDirection, Vector2 attackerPosition)
    {
        ShootArrow(attackDirection, attackerPosition);
    }

    private void Update()
    {
        AimBow();
    }

    private void ShootArrow(Vector2 shootDirection, Vector2 shooterPosition)
    {
        Debug.Log("shoot vector" +  shootDirection);
        GameObject arrow = Instantiate(ArrowPrefab, this.gameObject.transform.position, Quaternion.identity);
        ProjectileArrow projectileArrow = arrow.GetComponent<ProjectileArrow>();
        projectileArrow.InitializeArrow(shootDirection-shooterPosition, _arrowSpeed, _weaponUserController);

    }

    private void AimBow()
    {
        Vector3 aimDirection = (Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue()) - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
