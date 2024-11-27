using CharlieMadeAThing.NeatoTags.Core;
using UnityEngine;

public class SoftCollider : MonoBehaviour
{
    private float pushStrength = 1500f; // Strength of the pushing force
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.HasTag("Character"))
        {
            Debug.Log("enter character collision");
            // Check if the other object also has a Rigidbody2D
            Rigidbody2D otherRb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            if (otherRb != null)
            {
                ApplyPushForce(otherRb, collision);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.HasTag("Character"))
        {
            //Debug.Log("stay character collision");
            // Continuously apply force while colliding
            Rigidbody2D otherRb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            if (otherRb != null)
            {
                ApplyPushForce(otherRb, collision);
            }
        }
    }

    private void ApplyPushForce(Rigidbody2D otherRb, Collision2D collision)
    {
        // Calculate the direction of the push force
        Vector2 pushDirection = (otherRb.position - _rb.position).normalized;

        // Apply the force to both rigidbodies (equal and opposite reaction)
        _rb.AddForce(-pushDirection * pushStrength * Time.deltaTime, ForceMode2D.Force);
        otherRb.AddForce(pushDirection * pushStrength * Time.deltaTime, ForceMode2D.Force);
    }
}
