using CharlieMadeAThing.NeatoTags.Core;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private float contactKnockbackMagnitude = 15f;

    private EntityController _entityController;

    void Awake()
    {
        _entityController = GetComponentInParent<EntityController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.HasTag("Entity"))
        {
            if (_entityController.GetFactionID() != col.gameObject.GetComponent<EntityController>().GetFactionID())
            {
                _entityController.DealDamage(col.gameObject);
                _entityController.DealKnockback(col.gameObject, contactKnockbackMagnitude);
            }
        }
    }

}
