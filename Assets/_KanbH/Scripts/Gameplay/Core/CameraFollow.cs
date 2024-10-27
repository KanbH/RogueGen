using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float _smoothSpeed = 0.125f;

    private void Start()
    {
        transform.position = _followTarget.transform.position;
    }
    void LateUpdate()
    {
        if (_followTarget != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = _followTarget.transform.position + _offset;
            // Smoothly interpolate towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        _followTarget = newTarget;
    }
}
