using UnityEngine;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 10f;  // �������� ���������� ������
    public Vector3 offset = new Vector3(0, 5, -10);  // �������� ������

    private Transform target;  // ��������� ���������� ������

    void Start()
    {
        if (isLocalPlayer)
        {
            // ���� ��� ��������� �����, �� ������ ������ ��������� �� ���
            target = transform;  // ������ ������ �� ����� ����������� �������
        }
    }

    void LateUpdate()
    {
        if (!isLocalPlayer) return;

        // ������ ������ ��������� �� ������� � �������� ���������
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ������ ������ ������� �� ������
        transform.LookAt(target);
    }
}
