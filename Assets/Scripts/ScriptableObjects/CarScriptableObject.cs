using UnityEngine;

[CreateAssetMenu(fileName = "NewCarStats", menuName = "Cars/Stats")]
public class CarScriptableObject : ScriptableObject
{
    public float maxSpeed;
    public float turnSpeed;
    public float accelerationRate; // �������� �������
    public float decelerationRate; // �������� ����������

    public float drag; // ����������� ������ ��� ���������� ��� ��������
    public float brakeDeceleration; // ������� ���������� ��� ����������

    [Header("For BoxCollider")]
    public float SizeX = 2.7f;
    public float SizeY = 1.669491f;
    public double SizeZ = 6.609232;

}
