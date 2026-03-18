using UnityEngine;

public class DoorObject : MonoBehaviour
{

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameManager.GameState.Won)
        {
            transform.position += Vector3.up * 2f * Time.unscaledDeltaTime;
        }
    }
}
