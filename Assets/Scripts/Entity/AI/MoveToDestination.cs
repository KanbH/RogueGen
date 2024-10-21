using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDestination : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private AStarPathfinding _pathfinding;
    private List<Node> _path;
    private int _currentWaypointIndex;
    private Vector3 _targetPosition;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _pathfinding = GetComponent<AStarPathfinding>();
        
        _path = new List<Node>();
        _currentWaypointIndex = 0;
    }

    private void Update()
    {
        MoveAlongPath();
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;

        _path = _pathfinding.FindPath(this.transform.position, _targetPosition);
        _currentWaypointIndex = 0;
    }

    private void MoveAlongPath()
    {
        if (_currentWaypointIndex < _path.Count)
        {
            Node currentWaypoint = _path[_currentWaypointIndex];
            Vector3 targetWaypointPosition = currentWaypoint.worldPosition;

            Vector2 direction = (targetWaypointPosition - transform.position).normalized;

            _characterMovement.SetMovementDirection(direction);

            float distanceToWaypoint = Vector2.Distance(transform.position, targetWaypointPosition);

            if (distanceToWaypoint < 0.3f)
            {
                _currentWaypointIndex++;
            }
        }
        else if ( _currentWaypointIndex >= _path.Count)
        {
            _characterMovement.SetMovementDirection(Vector2.zero);
        }
        else 
        {
            _characterMovement.SetMovementDirection(Vector2.zero);
            _path.Clear();
        }
    }

    public bool IsOnTheWay()
    {
        return _currentWaypointIndex < _path.Count;
    }

    public bool HavePath()
    {
        return _path.Count == 0;
    }

    public void SetIdle()
    {
        _path.Clear();
        _characterMovement.SetMovementDirection(Vector2.zero);
    }
}
