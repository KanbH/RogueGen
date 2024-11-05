using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [SerializeField] public GameObject WeaponPrefab;

}
