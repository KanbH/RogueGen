using CharlieMadeAThing.NeatoTags.Core;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    [SerializeField] private float despawnTime;

    private float _speed;
    private Vector2 _direction;
    private EntityController _entityController;
    private Rigidbody2D _rigidbody2D;
    private bool initialized = false;

    public void InitializeArrow(Vector3 direction, float speed, EntityController entityController)
    {
        _speed = speed;
        _direction = direction.normalized;
        _entityController = entityController;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetRotation(_direction);
        initialized = true;

        Destroy(gameObject, despawnTime);
    }

    private void FixedUpdate()
    {
        if (initialized)
        {
            _rigidbody2D.linearVelocity = _direction * _speed;
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Collision logic with enemies, walls, or obstacles
        if (collision.gameObject.HasTag("Enemy"))
        {
            _entityController.DealDamage(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.HasTag("Player"))
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
