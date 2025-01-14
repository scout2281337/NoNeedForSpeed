using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPCarController : MonoBehaviour
{
    public CarScriptableObject[] carScripts;
    private CarScriptableObject carScript;

    [SerializeField] private float backAcceleration = 3f;

    private float currentSpeed = 0f; // Текущая скорость

    private Camera mainCam;
    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    private bool isBraking = false; // Флаг торможения

    private TextMeshProUGUI textMeshPro;

    [SerializeField] private GameObject[] cars;

    private void Awake()
    {
        carScript = carScripts[1];

        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("MainCamera not found! Make sure a camera with tag 'MainCamera' exists.");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Находим объект с тегом "Speed" и получаем компонент TextMeshProUGUI
        GameObject textObject = GameObject.FindGameObjectWithTag("Speed");
        if (textObject != null)
        {
            textMeshPro = textObject.GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on the object with tag 'Speed'.");
            }
        }
        else
        {
            Debug.LogError("Object with tag 'Speed' not found.");
        }
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Определение, тормозит ли игрок (пробел)
        isBraking = Input.GetKey(KeyCode.Space);
        DisplaySpeed();
    }

    private void FixedUpdate()
    {
        if (moveInput > 0)
        {
            // Ускорение вперёд
            if (currentSpeed >= 0)
            {
                currentSpeed += carScript.accelerationRate * Time.fixedDeltaTime;
            }
            else
            {
                // Замедление перед сменой направления
                currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.brakeDeceleration * Time.fixedDeltaTime);
                if (Mathf.Abs(currentSpeed) < 0.1f)
                {
                    currentSpeed = 0; // Обнуляем скорость для точности
                }
            }
            currentSpeed = Mathf.Clamp(currentSpeed, -carScript.maxSpeed / 2, carScript.maxSpeed);
        }
        else if (moveInput < 0)
        {
            // Ускорение назад
            if (currentSpeed <= 0)
            {
                currentSpeed -= backAcceleration * Time.fixedDeltaTime;
            }
            else
            {
                // Замедление перед сменой направления
                currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.brakeDeceleration * Time.fixedDeltaTime);
                if (Mathf.Abs(currentSpeed) < 0.1f)
                {
                    currentSpeed = 0; // Обнуляем скорость для точности
                }
            }
            currentSpeed = Mathf.Clamp(currentSpeed, -carScript.maxSpeed / 5, carScript.maxSpeed);
        }
        else
        {
            // Плавное замедление при отсутствии ввода
            currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.drag);
        }

        // Применение торможения (пробел)
        if (isBraking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.brakeDeceleration * Time.fixedDeltaTime);
        }

        // Применение движения
        Vector3 forwardForce = transform.forward * currentSpeed;
        rb.velocity = new Vector3(forwardForce.x, rb.velocity.y, forwardForce.z);

        // Управление поворотом
        if (currentSpeed > 0.1f)
        {
            float turn = turnInput * carScript.turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
        else if (currentSpeed < -0.1f)
        {
            float turn = -turnInput * carScript.turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // Камера движения
        CameraMovement();
    }


    private void CameraMovement()
    {
        if (mainCam == null) return;

        // Смещение камеры относительно машины
        Vector3 offset = new Vector3(0f, 5f, -10f);

        // Новая позиция камеры
        Vector3 targetPosition = transform.position + transform.rotation * offset;

        // Плавное перемещение камеры
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, targetPosition, 5f * Time.deltaTime);

        // Камера смотрит на машину
        mainCam.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    private void DisplaySpeed()
    {
        // Проверка, что textMeshPro не равен null
        if (textMeshPro != null)
        {
            // Получаем скорость в метрах в секунду
            float speedInMetersPerSecond = rb.velocity.magnitude;

            // Переводим в километры в час
            float speedInKilometersPerHour = (speedInMetersPerSecond * 3.6f) / 1;
            int SpeedForUI = (int)speedInKilometersPerHour;
            // Отображаем скорость на UI (с округлением до 2 знаков после запятой)
            textMeshPro.text =  SpeedForUI.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI component is not assigned.");
        }
    }

    public void SwitchToPantiac()
    {
        carScript = carScripts[0];
        cars[0].SetActive(true);
        cars[1].SetActive(false);
    }

    public void SwitchToMustang()
    {
        carScript = carScripts[1];
        cars[1].SetActive(true);
        cars[0].SetActive(false);
    }
}
