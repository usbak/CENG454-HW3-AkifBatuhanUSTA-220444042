using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool piercing = false; 

    private Rigidbody2D rb;
    private float spawnTime;

    public int Damage { get; set; }
    public bool Piercing { get; set; }

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    public void Launch(Vector2 direction, int dmg, bool isPiercing)
    {
        Damage = dmg;
        Piercing = isPiercing;
        rb.linearVelocity = direction.normalized * speed;
    }

    // === IPoolable ===
    public void OnSpawn()
    {
        spawnTime = Time.time;
        rb.linearVelocity = Vector2.zero;  // state reset
    }

    public void OnDespawn()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        
        if (Time.time - spawnTime > lifeTime)
            PoolManager.Instance.BulletPool.Return(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target) && target.IsAlive)
        {
            target.TakeDamage(Damage);

            
            PoolManager.Instance.HitEffectPool.Get(transform.position, Quaternion.identity);

            if (!Piercing)
                PoolManager.Instance.BulletPool.Return(this);
        }
    }
}