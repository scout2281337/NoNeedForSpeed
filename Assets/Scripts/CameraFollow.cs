using UnityEngine;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 10f;  // Скорость следования камеры
    public Vector3 offset = new Vector3(0, 5, -10);  // Смещение камеры

    private Transform target;  // Трансформ локального игрока

    void Start()
    {
        if (isLocalPlayer)
        {
            // Если это локальный игрок, то камера должна следовать за ним
            target = transform;  // Камера следит за своим собственным игроком
        }
    }

    void LateUpdate()
    {
        if (!isLocalPlayer) return;

        // Камера должна следовать за игроком с заданным смещением
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Камера всегда смотрит на игрока
        transform.LookAt(target);
    }
}
