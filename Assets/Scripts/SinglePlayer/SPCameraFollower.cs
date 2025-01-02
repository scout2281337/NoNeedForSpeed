using UnityEngine;

public class SPCameraFollower : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 10f;  // Скорость следования камеры
    public Vector3 offset = new Vector3(0, 4, -9);  // Смещение камеры

    private Transform target;  // Трансформ игрока

    void Start()
    {
        // Находим объект игрока для камеры
        target = transform;
        if (target == null)
        {
            Debug.LogError("Target for the camera is not assigned.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Камера должна следовать за игроком с заданным смещением
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Камера всегда смотрит на игрока
        transform.LookAt(target);
    }
}
