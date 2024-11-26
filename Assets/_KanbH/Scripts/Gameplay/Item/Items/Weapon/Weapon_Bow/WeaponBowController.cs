using UnityEngine;

public class WeaponBowController : WeaponController
{
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float _arrowSpeed;

    public override void UseWeapon(Vector2 attackDirection)
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
        EntityController entityController = GetComponentInParent<EntityController>();
        projectileArrow.InitializeArrow(shootDirection, _arrowSpeed, entityController);

    }

    private void AimBow()
    {
        Vector3 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
