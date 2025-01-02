using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("Wheel Transforms")]
    public Transform[] frontWheelTransforms; // Визуальные передние колеса
    public Transform[] rearWheelTransforms;  // Визуальные задние колеса

    [Header("Wheel Colliders")]
    public WheelCollider[] frontWheelColliders; // Коллайдеры передних колес
    public WheelCollider[] rearWheelColliders;  // Коллайдеры задних колес

    public float motorTorque = 1000f; // Мощность двигателя
    public float steerAngle = 30f;    // Угол поворота колес

    void Update()
    {
        // Обновление передних колес (поворот и вращение)
        for (int i = 0; i < frontWheelColliders.Length; i++)
        {
            UpdateSteeringWheel(frontWheelColliders[i], frontWheelTransforms[i]);
        }

        // Обновление всех колес (вращение)
        UpdateWheelRotation(frontWheelColliders, frontWheelTransforms);
        UpdateWheelRotation(rearWheelColliders, rearWheelTransforms);
    }

    private void UpdateSteeringWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // Получаем угол поворота переднего колеса
        float steer = Input.GetAxis("Horizontal") * steerAngle; // Управление с помощью клавиш A/D или стрелок

        // Устанавливаем угол поворота для коллайдера
        wheelCollider.steerAngle = steer;

        // Поворачиваем визуальное колесо в зависимости от steerAngle
        Vector3 localEulerAngles = wheelTransform.localEulerAngles;
        localEulerAngles.y = steer; // Устанавливаем поворот по оси Y (для поворота влево/вправо)
        wheelTransform.localEulerAngles = localEulerAngles;
    }

    private void UpdateWheelRotation(WheelCollider[] wheelColliders, Transform[] wheelTransforms)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            WheelCollider wheelCollider = wheelColliders[i];
            Transform wheelTransform = wheelTransforms[i];

            // Получаем вращение и позицию из WheelCollider
            wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            // Обновляем позицию и вращение визуального колеса
            wheelTransform.position = position;
            wheelTransform.rotation = rotation;

            // Вращаем колеса в зависимости от скорости (при движении вперед)
            if (wheelCollider.isGrounded)
            {
                wheelCollider.motorTorque = motorTorque * Input.GetAxis("Vertical"); // Вращение колес по оси "Vertical"
            }
        }
    }
}
