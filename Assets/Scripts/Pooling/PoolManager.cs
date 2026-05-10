using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Bullet Pool")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int bulletInitialSize = 30;

    [Header("Hit Effect Pool")]
    [SerializeField] private HitEffect hitEffectPrefab;
    [SerializeField] private int hitEffectInitialSize = 15;

    public ObjectPool<Bullet> BulletPool { get; private set; }
    public ObjectPool<HitEffect> HitEffectPool { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        BulletPool = new ObjectPool<Bullet>(bulletPrefab, bulletInitialSize, transform);
        HitEffectPool = new ObjectPool<HitEffect>(hitEffectPrefab, hitEffectInitialSize, transform);
    }
}