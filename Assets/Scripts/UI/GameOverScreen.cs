using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        restartButton.onClick.AddListener(() => GameManager.Instance.Restart());
    }

    private void OnEnable()
    {
        GameEvents.OnAllWavesCompleted += ShowWin;
        GameEvents.OnCoreDestroyed += ShowLose;
    }
    private void OnDisable()
    {
        GameEvents.OnAllWavesCompleted -= ShowWin;
        GameEvents.OnCoreDestroyed -= ShowLose;
    }

    private void ShowWin()
    {
        winPanel.SetActive(true);
        finalScoreText.text = $"FINAL SCORE: {GameManager.Instance.Score}";
    }

    private void ShowLose()
    {
        losePanel.SetActive(true);
        finalScoreText.text = $"FINAL SCORE: {GameManager.Instance.Score}";
    }
}