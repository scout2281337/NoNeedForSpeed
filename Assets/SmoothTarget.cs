using UnityEngine;
public class SmoothTarget : MonoBehaviour
{
    public Transform car; // Машина
    public float smoothSpeed = 0.1f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = car.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
