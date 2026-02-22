using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forceMagnitude = 10f;
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down
        Vector3 force = new Vector3(h, 0, v) * forceMagnitude;
        rb.AddForce(force);
    }
}