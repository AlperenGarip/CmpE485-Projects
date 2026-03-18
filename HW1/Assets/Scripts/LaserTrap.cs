using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserTrap : MonoBehaviour
{
    [Header("Laser Points")]
    public Transform laserEnd;

    [Header("Timing (Coroutine)")]
    public float activeTime   = 2f;
    public float inactiveTime = 1.5f;

    [Header("Visuals")]
    public Color laserColor = Color.red;
    public float laserWidth = 0.08f;
    public float pulseSpeed = 8f;

    private LineRenderer lineRenderer;
    private bool isActive = true;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth    = laserWidth;
        lineRenderer.endWidth      = laserWidth;
        lineRenderer.material      = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = laserColor;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    void Start()
    {
        StartCoroutine(LaserCycle());
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            SetLaser(true);
            yield return new WaitForSeconds(activeTime);

            SetLaser(false);
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    void SetLaser(bool on)
    {
        isActive = on;
        lineRenderer.enabled = on;
    }

    void Update()
    {
        if (laserEnd == null) return;

        // ── Visuals ────────────────────────────────────────────────────────
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserEnd.position);

        if (isActive)
        {
            float intensity = 0.6f + 0.4f * Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));
            lineRenderer.material.color = laserColor * intensity;
        }

        // ── Detection via Raycast (no tunneling, any angle) ───────────────
        if (!isActive) return;
        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        Vector3 origin    = transform.position;
        Vector3 direction = (laserEnd.position - origin);
        float   length    = direction.magnitude;
        direction /= length; // normalise

        RaycastHit[] hits = Physics.RaycastAll(origin, direction, length);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Laser hit the player!");
                GameManager.Instance.LoseGame();
                return;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (laserEnd == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, laserEnd.position);
    }
}
