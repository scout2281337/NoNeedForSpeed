using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("Wheel Transforms")]
    public Transform[] frontWheelTransforms; // ���������� �������� ������
    public Transform[] rearWheelTransforms;  // ���������� ������ ������

    [Header("Wheel Colliders")]
    public WheelCollider[] frontWheelColliders; // ���������� �������� �����
    public WheelCollider[] rearWheelColliders;  // ���������� ������ �����

    public float motorTorque = 1000f; // �������� ���������
    public float steerAngle = 30f;    // ���� �������� �����

    void Update()
    {
        // ���������� �������� ����� (������� � ��������)
        for (int i = 0; i < frontWheelColliders.Length; i++)
        {
            UpdateSteeringWheel(frontWheelColliders[i], frontWheelTransforms[i]);
        }

        // ���������� ���� ����� (��������)
        UpdateWheelRotation(frontWheelColliders, frontWheelTransforms);
        UpdateWheelRotation(rearWheelColliders, rearWheelTransforms);
    }

    private void UpdateSteeringWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // �������� ���� �������� ��������� ������
        float steer = Input.GetAxis("Horizontal") * steerAngle; // ���������� � ������� ������ A/D ��� �������

        // ������������� ���� �������� ��� ����������
        wheelCollider.steerAngle = steer;

        // ������������ ���������� ������ � ����������� �� steerAngle
        Vector3 localEulerAngles = wheelTransform.localEulerAngles;
        localEulerAngles.y = steer; // ������������� ������� �� ��� Y (��� �������� �����/������)
        wheelTransform.localEulerAngles = localEulerAngles;
    }

    private void UpdateWheelRotation(WheelCollider[] wheelColliders, Transform[] wheelTransforms)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            WheelCollider wheelCollider = wheelColliders[i];
            Transform wheelTransform = wheelTransforms[i];

            // �������� �������� � ������� �� WheelCollider
            wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            // ��������� ������� � �������� ����������� ������
            wheelTransform.position = position;
            wheelTransform.rotation = rotation;

            // ������� ������ � ����������� �� �������� (��� �������� ������)
            if (wheelCollider.isGrounded)
            {
                wheelCollider.motorTorque = motorTorque * Input.GetAxis("Vertical"); // �������� ����� �� ��� "Vertical"
            }
        }
    }
}
