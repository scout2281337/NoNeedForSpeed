using UnityEngine;

public class SPCameraFollower : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 10f;  // �������� ���������� ������
    public Vector3 offset = new Vector3(0, 4, -9);  // �������� ������

    private Transform target;  // ��������� ������

    void Start()
    {
        // ������� ������ ������ ��� ������
        target = transform;
        if (target == null)
        {
            Debug.LogError("Target for the camera is not assigned.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ������ ������ ��������� �� ������� � �������� ���������
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ������ ������ ������� �� ������
        transform.LookAt(target);
    }
}
