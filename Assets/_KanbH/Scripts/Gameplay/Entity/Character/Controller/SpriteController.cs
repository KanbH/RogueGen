using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Sprite NorthDirectionSprite;
    [SerializeField] private Sprite SouthDirectionSprite;
    [SerializeField] private Sprite EastDirectionSprite;
    [SerializeField] private Sprite WestDirectionSprite;

    private SpriteRenderer _spriteRenderer;

    public enum SpriteDirection { North, South, East, West }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteDirection(Vector2 vector)
    {
        SpriteDirection spriteDirection = Vector2ToSpriteDirection(vector);
        // Set sprite based on direction
        switch (spriteDirection)
        {
            case SpriteDirection.North:
                _spriteRenderer.sprite = NorthDirectionSprite;
                break;
            case SpriteDirection.South:
                _spriteRenderer.sprite = SouthDirectionSprite;
                break;
            case SpriteDirection.East:
                _spriteRenderer.sprite = EastDirectionSprite;
                break;
            case SpriteDirection.West:
                _spriteRenderer.sprite = WestDirectionSprite;
                break;
        }
    }

    public SpriteDirection Vector2ToSpriteDirection(Vector2 vector)
    {
        // Normalize the input vector for direction calculation
        vector = vector.normalized;

        // Determine if the vector is diagonal (where x and y magnitudes are roughly equal)
        bool isDiagonal = Mathf.Approximately(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

        // Handle diagonal case, choose East or West based on x-component
        if (isDiagonal)
        {
            return vector.x > 0 ? SpriteDirection.East : SpriteDirection.West;
        }

        // Calculate the main direction based on x and y values
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            // Horizontal: East or West
            return vector.x > 0 ? SpriteDirection.East : SpriteDirection.West;
        }
        else
        {
            // Vertical: North or South
            return vector.y > 0 ? SpriteDirection.North : SpriteDirection.South;
        }
    }
}
