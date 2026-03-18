using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float mouseSensitivity = 3f;
    public float distance         = 6f;
    public float minPitch         = 10f;   // Lowest angle (degrees above horizon)
    public float maxPitch         = 70f;   // Highest angle (looking down steeply)

    [Header("Zoom")]
    public float minDistance = 2f;
    public float maxDistance = 12f;
    public float zoomSpeed   = 3f;

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    [Header("Look")]
    public float lookAtHeightOffset = 1f;

    // Current orbit angles
    private float currentYaw;
    private float currentPitch = 30f;

    // Smoothed camera position
    private Vector3 smoothVelocity;

    void Start()
    {
        if (target != null)
            currentYaw = target.eulerAngles.y;

        // Lock and hide cursor for a clean game feel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        // Auto-unlock cursor when the game ends so UI buttons are clickable
        bool gameOver = GameManager.Instance != null &&
                        GameManager.Instance.CurrentState != GameManager.GameState.Playing;
        if (gameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible   = true;
            return; // don't process camera input while in UI
        }

        // Manual unlock with Escape during gameplay
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible   = true;
        }
        // Re-lock on click (only during active gameplay)
        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible   = false;
        }

        // Don't rotate camera if cursor is unlocked (player is clicking UI)
        if (Cursor.lockState != CursorLockMode.Locked) return;

        // Read mouse delta
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        currentYaw   += mouseX;
        currentPitch -= mouseY;                              // inverted so mouse up = look up
        currentPitch  = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Scroll wheel zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Build the desired camera position from orbit angles
        Quaternion rotation    = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3    desiredPos  = target.position
                               + rotation * new Vector3(0f, 0f, -distance)
                               + Vector3.up * 0f; // pitch already handles height

        // Smooth position
        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPos, ref smoothVelocity, 1f / smoothSpeed);

        // Look at the player
        transform.LookAt(target.position + Vector3.up * lookAtHeightOffset);
    }
}
