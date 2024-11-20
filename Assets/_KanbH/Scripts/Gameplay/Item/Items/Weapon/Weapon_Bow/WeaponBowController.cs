using UnityEngine;

public class WeaponBowController : WeaponController
{
    [SerializeField] private GameObject ArrowPrefab;

    public override void UseWeapon(Vector2 attackDirection)
    {
        
    }

    private void Update()
    {
        AimBow();
    }

    private void ShootArrow(Vector2 shootDirection, GameObject arrowPrefab)
    {

    }

    private void AimBow()
    {
        Vector3 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, angle);
    }

}
