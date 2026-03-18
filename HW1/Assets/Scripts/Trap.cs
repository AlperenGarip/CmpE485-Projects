using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{
    [Header("Trap Timing")]
    public float activeTime = 3f;   // How long the trap floor is visible
    public float inactiveTime = 2f; // How long the trap floor disappears

    private MeshRenderer meshRenderer;
    private Collider trapCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        trapCollider = GetComponent<Collider>();
        StartCoroutine(TrapCycle());
    }

    IEnumerator TrapCycle()
    {
        while (true)
        {
            // Floor is active (safe to walk on)
            SetTrapActive(true);
            yield return new WaitForSeconds(activeTime);

            // Floor disappears (player falls if standing here)
            SetTrapActive(false);
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    void SetTrapActive(bool active)
    {
        if (meshRenderer != null) meshRenderer.enabled = active;
        if (trapCollider != null) trapCollider.enabled = active;
    }
}
