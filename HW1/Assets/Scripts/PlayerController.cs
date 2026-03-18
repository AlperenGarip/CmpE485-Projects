using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Freeze rotation so the player doesn't topple
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get camera-relative directions (ignoring Y)
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal);

        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();

            // Move using Rigidbody velocity (keep existing Y velocity for gravity)
            Vector3 newVelocity = moveDirection * moveSpeed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;

            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Stop horizontal movement when no input
            Vector3 vel = rb.velocity;
            vel.x = 0f;
            vel.z = 0f;
            rb.velocity = vel;
        }
    }
}
