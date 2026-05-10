using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider coreHealthBar;
    [SerializeField] private Slider playerHealthBar;
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
        coreHealthBar.value = (float)hp / max;
    }

    private void HandlePlayerHealth(int hp, int max)
    {
        playerHealthBar.value = (float)hp / max;
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