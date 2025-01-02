using UnityEngine;

public class CarSteering : MonoBehaviour
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    public float steeringAngle = 30f; // максимальный угол поворота колес
    public float steeringSpeed = 5f; // скорость поворота колес

    private float currentSteeringAngle = 0f;

    void Update()
    {
        SteerWheels();
    }

    void SteerWheels()
    {
        // ѕолучаем ввод дл€ поворота (например, с клавиш "A" и "D" или стрелок)
        float horizontalInput = Input.GetAxis("Horizontal");

        // –ассчитываем угол поворота колес
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, horizontalInput * steeringAngle, steeringSpeed * Time.deltaTime);

        // ѕримен€ем угол поворота к передним колесам
        frontLeftWheel.localRotation = Quaternion.Euler(0f, currentSteeringAngle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, currentSteeringAngle, 0f);
    }
}
