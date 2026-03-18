using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, Won, Lost }
    public GameState CurrentState { get; private set; } = GameState.Playing;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void WinGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Won;
        Debug.Log("YOU WIN!");
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
    }

    public void LoseGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Lost;
        Debug.Log("YOU DIED!");
        Time.timeScale = 0f;
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
