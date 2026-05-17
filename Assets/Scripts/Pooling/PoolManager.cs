using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Bullet Pool")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int bulletInitialSize = 30;

    public ObjectPool<Bullet> BulletPool { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        BulletPool = new ObjectPool<Bullet>(bulletPrefab, bulletInitialSize, transform);
    }
}