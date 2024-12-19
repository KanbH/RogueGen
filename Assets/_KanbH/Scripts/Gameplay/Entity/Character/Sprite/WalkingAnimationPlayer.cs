using UnityEngine;

public class WalkingAnimationPlayer : WalkingAnimation
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _entityMovement = GetComponentInParent<EntityMovement>();
        _currentWalkingSprite = SouthWalkingSprite;
    }

    // Update is called once per frame
    void Update()
    {
        _movement = _entityMovement.GetMovement();
        _lookingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        if (_movement.magnitude > 0)
        {
            _facingDirection = Vector2ToSpriteDirection(_lookingDirection);
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
            else
            {
                _facingDirection = Vector2ToSpriteDirection(_lookingDirection);
                ChangeSpriteDirection(_facingDirection);
                _spriteRenderer.sprite = _currentWalkingSprite[0];
            }
        }
    }
}
