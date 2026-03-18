using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 90f;   // Degrees per second around Y axis

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        rb.angularVelocity = new Vector3(
            rb.angularVelocity.x,
            rotationSpeed * Mathf.Deg2Rad,   // convert deg/s → rad/s
            rb.angularVelocity.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("Key hit the Door! Game Won!");
            if (GameManager.Instance != null)
                GameManager.Instance.WinGame();
        }
    }
}
