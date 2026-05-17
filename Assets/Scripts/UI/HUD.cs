using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coreHealthText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;

    private void OnEnable()
    {
        GameEvents.OnCoreDamaged += HandleCoreDamaged;
        GameEvents.OnPlayerHealthChanged += HandlePlayerHealth;
        GameEvents.OnScoreChanged += HandleScore;
        GameEvents.OnWaveStarted += HandleWave;
    }

    private void OnDisable()
    {
        
        GameEvents.OnCoreDamaged -= HandleCoreDamaged;
        GameEvents.OnPlayerHealthChanged -= HandlePlayerHealth;
        GameEvents.OnScoreChanged -= HandleScore;
        GameEvents.OnWaveStarted -= HandleWave;
    }

    private void HandleCoreDamaged(int hp, int max)
    {
        coreHealthText.text = $"CORE: {hp} / {max}";
    }

    private void HandlePlayerHealth(int hp, int max)
    {
        playerHealthText.text = $"HP: {hp} / {max}";
    }

    private void HandleScore(int score)
    {
        scoreText.text = $"SCORE: {score:000000}";
    }

    private void HandleWave(int idx)
    {
        waveText.text = $"WAVE {idx + 1} / 3";
    }
}