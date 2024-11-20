using CharlieMadeAThing.NeatoTags.Core;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    [SerializeField] private float despawnTime;

    private float _speed;
    private Vector2 _direction;
    private EntityController _entityController;
    private bool initialized = false;

    public void InitializeArrow(Vector3 direction, float speed, EntityController entityController)
    {
        _speed = speed;
        _direction = direction;
        _entityController = entityController;
        SetRotation(_direction);
        initialized = true;

        Destroy(gameObject, despawnTime);
    }

    private void Update()
    {
        if (initialized)
        {
            transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Collision logic with enemies, walls, or obstacles
        if (other.gameObject.HasTag("Enemy"))
        {
            _entityController.DealDamage(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.HasTag("Player"))
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetRotation(Vector3 direction)
    {
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a Quaternion rotation
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Apply the rotation to the object
        transform.rotation = rotation;
    }
}
