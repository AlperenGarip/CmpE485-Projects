using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    [Header("Detection")]
    public float detectionRange = 2.5f;
    public Transform player;

    private int currentWaypointIndex = 0;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            StartCoroutine(Patrol());
        }

        // Try to find the player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (waypoints.Length == 0) yield break;

            Transform target = waypoints[currentWaypointIndex];

            // Move toward the current waypoint
            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Face movement direction
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
                }

                yield return null;
            }

            // Move to next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // Small pause at each waypoint
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if (player == null) return;
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        // Check if the player is too close
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            Debug.Log("Guard caught the player!");
            GameManager.Instance.LoseGame();
        }
    }

    // Visualize detection range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
