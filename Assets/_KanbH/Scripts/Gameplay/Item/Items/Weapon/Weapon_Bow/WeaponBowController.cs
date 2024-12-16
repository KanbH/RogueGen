using UnityEngine;

public class WeaponBowController : WeaponController
{
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float _arrowSpeed;

    public override void UseWeapon(Vector2 attackDirection, Vector2 attackerTransform)
    {
        ShootArrow(attackDirection);
    }

    private void Update()
    {
        AimBow();
    }

    private void ShootArrow(Vector2 shootDirection)
    {
        GameObject arrow = Instantiate(ArrowPrefab, this.gameObject.transform.position, Quaternion.identity);
        ProjectileArrow projectileArrow = arrow.GetComponent<ProjectileArrow>();
        projectileArrow.InitializeArrow(shootDirection, _arrowSpeed, _weaponUserController);

    }

    private void AimBow()
    {
        Vector3 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
