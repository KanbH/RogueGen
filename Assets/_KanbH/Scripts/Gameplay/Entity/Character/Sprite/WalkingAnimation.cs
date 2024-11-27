using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> NorthWalkingSprite;
    [SerializeField] private List<Sprite> SouthWalkingSprite;
    [SerializeField] private List<Sprite> EastWalkingSprite;
    [SerializeField] private List<Sprite> WestWalkingSprite;
    [SerializeField] private float frameRate = 0.1f;

    private enum SpriteDirection { North, South, East, West }
    private SpriteDirection _facingDirection = SpriteDirection.South;
    private List<Sprite> _currentWalkingSprite;

    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;
    private Vector2 _movement;
    private Vector2 _direction;

    private int _currentSpriteIndex = 0;
    private bool _isWalking = false;
    private Coroutine _walkCycleCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _currentWalkingSprite = SouthWalkingSprite;
    }

    void Update()
    {
        //Check if the character is moving (replace with your movement logic)
        _movement = _playerMovement.GetMovement();
        _direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        if (_movement.magnitude > 0)
        {
            _facingDirection = Vector2ToSpriteDirection(_direction);
            ChangeSpriteDirection(_facingDirection);
            //Debug.Log("Player is moving, switching sprite...");
            if (!_isWalking)
            {
                StartWalking();
            }
        }
        else
        {
            //Debug.Log("Player is not moving, stop switching sprite...");
            if (_isWalking)
            {
                StopWalking();
            }
        }
    }

    private void StartWalking()
    {
        _isWalking = true;

        if (_walkCycleCoroutine != null)
            StopCoroutine(_walkCycleCoroutine);

        _walkCycleCoroutine = StartCoroutine(CycleSprites());
    }

    private void StopWalking()
    {
        _isWalking = false;

        if (_walkCycleCoroutine != null)
        {
            StopCoroutine(_walkCycleCoroutine);
            _walkCycleCoroutine = null;
        }

        // Reset to a default idle sprite (optional)
        if (_currentWalkingSprite.Count > 0)
            _spriteRenderer.sprite = _currentWalkingSprite[0];
    }

    private IEnumerator CycleSprites()
    {
        while (_isWalking)
        {
            // Update the sprite to the next in the list
            _spriteRenderer.sprite = _currentWalkingSprite[_currentSpriteIndex];

            // Move to the next index, looping back to the start if necessary
            _currentSpriteIndex = (_currentSpriteIndex + 1) % _currentWalkingSprite.Count;

            // Wait for the next frame
            yield return new WaitForSeconds(frameRate);
        }
    }

    private SpriteDirection Vector2ToSpriteDirection(Vector2 vector)
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
        else if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
        {
            // Vertical: North or South
            return vector.y > 0 ? SpriteDirection.North : SpriteDirection.South;
        }
        else return _facingDirection;
    }

    private void ChangeSpriteDirection(SpriteDirection spriteDirection)
    {
        // Set sprite based on direction
        switch (spriteDirection)
        {
            case SpriteDirection.North:
                _currentWalkingSprite = NorthWalkingSprite;
                break;
            case SpriteDirection.South:
                _currentWalkingSprite = SouthWalkingSprite;
                break;
            case SpriteDirection.East:
                _currentWalkingSprite = EastWalkingSprite;
                break;
            case SpriteDirection.West:
                _currentWalkingSprite = WestWalkingSprite;
                break;
        }
    }
}
