using System.Collections;
using UnityEngine;

[System.Serializable]
public class WaveConfig
{
    public int straightCount = 5;
    public int zigzagCount = 2;
    public float spawnInterval = 0.6f;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform coreTransform;
    [SerializeField] private WaveConfig[] waves;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float waveBreak = 3f;

    private int aliveEnemies;
    private int currentWaveIndex;

    private void OnEnable() => GameEvents.OnEnemyDied += HandleEnemyDied;
    private void OnDisable() => GameEvents.OnEnemyDied -= HandleEnemyDied;

    private void Start() => StartCoroutine(RunWaves());

    private IEnumerator RunWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            currentWaveIndex = i;
            GameEvents.RaiseWaveStarted(i);

            var w = waves[i];
            aliveEnemies = w.straightCount + w.zigzagCount;

            for (int s = 0; s < w.straightCount; s++)
            {
                SpawnEnemy(EnemyMovementType.Straight);
                yield return new WaitForSeconds(w.spawnInterval);
            }
            for (int z = 0; z < w.zigzagCount; z++)
            {
                SpawnEnemy(EnemyMovementType.ZigZag);
                yield return new WaitForSeconds(w.spawnInterval);
            }

            // Tüm enemy ölene kadar bekle
            yield return new WaitUntil(() => aliveEnemies <= 0);
            GameEvents.RaiseWaveCompleted(i);
            yield return new WaitForSeconds(waveBreak);
        }

        GameEvents.RaiseAllWavesCompleted();
    }

    private void SpawnEnemy(EnemyMovementType type)
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 pos = coreTransform.position +
            new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

        Enemy e = Instantiate(enemyPrefab, pos, Quaternion.identity);
        e.Initialize(coreTransform, type);
    }

    private void HandleEnemyDied(Vector2 _) => aliveEnemies--;
}