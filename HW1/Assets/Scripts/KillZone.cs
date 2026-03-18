using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell into the kill zone!");
            if (GameManager.Instance != null)
                GameManager.Instance.LoseGame();
        }
        else if (other.CompareTag("Key"))
        {
            Debug.Log("Key fell into the kill zone! Game Over!");
            if (GameManager.Instance != null)
                GameManager.Instance.LoseGame();
        }
    }
}
