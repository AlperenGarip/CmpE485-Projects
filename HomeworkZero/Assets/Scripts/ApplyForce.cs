using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public Rigidbody rb; // drag your Rigidbody here in Inspector
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * 3f); // pushes forward every physics frame
    }
}