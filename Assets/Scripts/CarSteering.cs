using UnityEngine;

public class CarSteering : MonoBehaviour
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    public float steeringAngle = 30f; // ������������ ���� �������� �����
    public float steeringSpeed = 5f; // �������� �������� �����

    private float currentSteeringAngle = 0f;

    void Update()
    {
        SteerWheels();
    }

    void SteerWheels()
    {
        // �������� ���� ��� �������� (��������, � ������ "A" � "D" ��� �������)
        float horizontalInput = Input.GetAxis("Horizontal");

        // ������������ ���� �������� �����
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, horizontalInput * steeringAngle, steeringSpeed * Time.deltaTime);

        // ��������� ���� �������� � �������� �������
        frontLeftWheel.localRotation = Quaternion.Euler(0f, currentSteeringAngle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, currentSteeringAngle, 0f);
    }
}
