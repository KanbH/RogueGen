using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target; // What GameObject the camera will follow
    public Vector3 offset = new Vector3(0f,0f,-10f); // Offset between the camera and the GameObject
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = target.transform.position + offset;
            // Smoothly interpolate towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
