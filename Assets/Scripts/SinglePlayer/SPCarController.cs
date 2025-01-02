using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPCarController : MonoBehaviour
{
    public CarScriptableObject[] carScripts;
    private CarScriptableObject carScript;

    [SerializeField] private float backAcceleration = 30f;

    private float currentSpeed = 0f; // ������� ��������

    private Camera mainCam;
    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    private bool isBraking = false; // ���� ����������

    private TextMeshProUGUI textMeshPro;

    [SerializeField] private GameObject[] cars;

    private void Awake()
    {
        carScript = carScripts[0];

        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("MainCamera not found! Make sure a camera with tag 'MainCamera' exists.");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ������� ������ � ����� "Speed" � �������� ��������� TextMeshProUGUI
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

        // �����������, �������� �� ����� (������)
        isBraking = Input.GetKey(KeyCode.Space);
        DisplaySpeed();
    }

    private void FixedUpdate()
    {
        // ��������� � ����������
        if (moveInput > 0)
        {
            currentSpeed += carScript.accelerationRate * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, carScript.maxSpeed);
        }
        else if (moveInput < 0)
        {
            currentSpeed -= carScript.decelerationRate * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -carScript.maxSpeed / 2, 0);
        }
        else
        {
            // ������� ����������, ����� ��� �������
            currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.drag);
        }

        // ��� ���������� (������) ��������� �������� �������
        if (isBraking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, carScript.brakeDeceleration * Time.fixedDeltaTime);
        }

        // ���������� �������� ������/�����
        Vector3 forwardForce = transform.forward * currentSpeed;
        rb.velocity = new Vector3(forwardForce.x, rb.velocity.y, forwardForce.z); // ��������� ������������ ��������

        // ���������� ���������
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

        // ������ ��������
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (mainCam == null) return;

        // �������� ������ ������������ ������
        Vector3 offset = new Vector3(0f, 5f, -10f);

        // ����� ������� ������
        Vector3 targetPosition = transform.position + transform.rotation * offset;

        // ������� ����������� ������
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, targetPosition, 5f * Time.deltaTime);

        // ������ ������� �� ������
        mainCam.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    private void DisplaySpeed()
    {
        // ��������, ��� textMeshPro �� ����� null
        if (textMeshPro != null)
        {
            // �������� �������� � ������ � �������
            float speedInMetersPerSecond = rb.velocity.magnitude;

            // ��������� � ��������� � ���
            float speedInKilometersPerHour = speedInMetersPerSecond * 3.6f;

            // ���������� �������� �� UI (� ����������� �� 2 ������ ����� �������)
            textMeshPro.text = "Speed: " + speedInKilometersPerHour.ToString("F2") + " km/h";
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
