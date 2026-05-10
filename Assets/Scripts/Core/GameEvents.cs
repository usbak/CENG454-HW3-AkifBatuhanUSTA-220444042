using System;
using UnityEngine;


public static class GameEvents
{
    // === CORE EVENTS ===
    public static event Action<int, int> OnCoreDamaged;   // (newHP, maxHP)
    public static event Action OnCoreDestroyed;

    // === ENEMY EVENTS ===
    public static event Action<Vector2> OnEnemyDied;       // position
    public static event Action<int> OnScoreChanged;        // totalScore

    // === WAVE EVENTS ===
    public static event Action<int> OnWaveStarted;         // waveIndex
    public static event Action<int> OnWaveCompleted;       // waveIndex
    public static event Action OnAllWavesCompleted;          // WIN

    // === PLAYER EVENTS ===
    public static event Action<int, int> OnPlayerHealthChanged;
    public static event Action<string> OnWeaponUpgraded;     // upgrade name

    
    public static void RaiseCoreDamaged(int newHP, int maxHP) =>
        OnCoreDamaged?.Invoke(newHP, maxHP);

    public static void RaiseCoreDestroyed() =>
        OnCoreDestroyed?.Invoke();

    public static void RaiseEnemyDied(Vector2 pos) =>
        OnEnemyDied?.Invoke(pos);

    public static void RaiseScoreChanged(int score) =>
        OnScoreChanged?.Invoke(score);

    public static void RaiseWaveStarted(int idx) =>
        OnWaveStarted?.Invoke(idx);

    public static void RaiseWaveCompleted(int idx) =>
        OnWaveCompleted?.Invoke(idx);

    public static void RaiseAllWavesCompleted() =>
        OnAllWavesCompleted?.Invoke();

    public static void RaisePlayerHealthChanged(int hp, int max) =>
        OnPlayerHealthChanged?.Invoke(hp, max);

    public static void RaiseWeaponUpgraded(string name) =>
        OnWeaponUpgraded?.Invoke(name);

    public static void ClearAll()
    {
        OnCoreDamaged = null;
        OnCoreDestroyed = null;
        OnEnemyDied = null;
        OnScoreChanged = null;
        OnWaveStarted = null;
        OnWaveCompleted = null;
        OnAllWavesCompleted = null;
        OnPlayerHealthChanged = null;
        OnWeaponUpgraded = null;
    }
}
