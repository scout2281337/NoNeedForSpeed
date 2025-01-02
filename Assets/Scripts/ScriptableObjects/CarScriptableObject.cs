using UnityEngine;

[CreateAssetMenu(fileName = "NewCarStats", menuName = "Cars/Stats")]
public class CarScriptableObject : ScriptableObject
{
    public float maxSpeed;
    public float turnSpeed;
    public float accelerationRate; // Скорость разгона
    public float decelerationRate; // Скорость торможения

    public float drag; // Коэффициент трения для замедления без тормозов
    public float brakeDeceleration; // Плавное замедление при торможении

    [Header("For BoxCollider")]
    public float SizeX = 2.7f;
    public float SizeY = 1.669491f;
    public double SizeZ = 6.609232;

}
