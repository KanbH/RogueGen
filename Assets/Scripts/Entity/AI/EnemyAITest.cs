using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITest : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private AStarPathfinding _pathfinding; // Reference to your A* pathfinding script
    private List<Node> _path;              // The current path the enemy will follow
    private int _currentWaypointIndex;     // The index of the current waypoint in the path
    private Vector3 _targetPosition;       // The final target position

    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _pathfinding = GetComponent<AStarPathfinding>(); // Find A* script in the scene
        _path = new List<Node>();

        
    }

    void Update()
    {
        // Detect mouse click and set target position
        if (Input.GetMouseButtonDown(0)) // Left-click to set target
        {
            SetTargetPosition();
        }

        // Follow the path if one exists
        if (_path != null && _path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    private void SetTargetPosition()
    {
        // Convert the mouse position to world position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // Ensure Z is 0 since it's a 2D game

        // Set the target position for pathfinding
        _targetPosition = mouseWorldPosition;

        // Convert world position to grid coordinates and run A* pathfinding
        _path = _pathfinding.FindPath(this.transform.position, _targetPosition);
        _currentWaypointIndex = 0; // Reset to the first waypoint

        if (_path == null || _path.Count == 0)
        {
            Debug.Log("No valid path to the target.");
        }
        else
        {
            Debug.Log("Path found with " + _path.Count + " waypoints.");
        }
    }

    private void MoveAlongPath()
    {
        // Get the current waypoint in world position
        if (_currentWaypointIndex < _path.Count)
        {
            Node currentWaypoint = _path[_currentWaypointIndex];
            Vector3 targetWaypointPosition = currentWaypoint.worldPosition;

            // Calculate direction to the next waypoint
            Vector2 direction = (targetWaypointPosition - transform.position).normalized;

            // Set the movement direction for CharacterMovement
            _characterMovement.SetMovementDirection(direction);

            // Check if the enemy is close enough to the waypoint to move to the next
            float distanceToWaypoint = Vector2.Distance(transform.position, targetWaypointPosition);

            // If the enemy is close enough, move to the next waypoint
            if (distanceToWaypoint < 0.3f)
            {
                _currentWaypointIndex++;
            }
        }
        else
        {
            // Reached the destination
            _characterMovement.SetMovementDirection(Vector2.zero); // Stop moving
            Debug.Log("Reached the target position.");
        }
    }
}
