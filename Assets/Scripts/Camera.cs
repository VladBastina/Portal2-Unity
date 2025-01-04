using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;          // Base speed for WASD
    [SerializeField] private float sprintMultiplier = 2f;   // Hold Shift for faster movement

    [Header("Vertical Controls (Optional)")]
    [Tooltip("Set keys (Q/E) to move up/down if desired.")]
    [SerializeField] private bool useVerticalKeys = true;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 100f; // Mouse sensitivity for look
    [SerializeField] private float lookXLimit = 90f;        // Vertical rotation clamp

    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // ----------------------
        // 2. MOVEMENT (WASD + optional Q/E)
        // ----------------------
        float moveForward = Input.GetAxis("Vertical");   // W/S
        float moveSide = Input.GetAxis("Horizontal");    // A/D

        // Optional up/down movement (Q/E) if enabled
        float moveUpDown = 0f;
        if (useVerticalKeys)
        {
            if (Input.GetKey(KeyCode.E)) moveUpDown = 1f;   // Move up
            if (Input.GetKey(KeyCode.Q)) moveUpDown = -1f;  // Move down
        }

        // Sprint if Shift is held
        float currentSpeed = Input.GetKey(KeyCode.LeftShift)
            ? moveSpeed * sprintMultiplier
            : moveSpeed;

        // Direction relative to camera orientation
        Vector3 move = transform.forward * moveForward
                     + transform.right * moveSide
                     + transform.up * moveUpDown;

        // Apply movement
        transform.position += move * currentSpeed * Time.deltaTime;
    }
}
