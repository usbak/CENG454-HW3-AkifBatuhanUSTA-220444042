using UnityEngine;

public enum GameState { Playing, Won, Lost }


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Playing;
    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnCoreDestroyed += HandleLose;
        GameEvents.OnAllWavesCompleted += HandleWin;
        GameEvents.OnScoreChanged += HandleScore;
    }
    private void OnDisable()
    {
        GameEvents.OnCoreDestroyed -= HandleLose;
        GameEvents.OnAllWavesCompleted -= HandleWin;
        GameEvents.OnScoreChanged -= HandleScore;
    }

    private void HandleScore(int delta) { Score += delta; }
    private void HandleWin()  { State = GameState.Won;  Time.timeScale = 0f; }
    private void HandleLose() { State = GameState.Lost; Time.timeScale = 0f; }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameEvents.ClearAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}