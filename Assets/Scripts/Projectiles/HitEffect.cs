using UnityEngine;

public class HitEffect : MonoBehaviour, IPoolable
{
    [SerializeField] private float duration = 0.3f;
    private float spawnTime;

    public void OnSpawn() => spawnTime = Time.time;
    public void OnDespawn() { }

    private void Update()
    {
        if (Time.time - spawnTime > duration)
            PoolManager.Instance.HitEffectPool.Return(this);
    }
}
