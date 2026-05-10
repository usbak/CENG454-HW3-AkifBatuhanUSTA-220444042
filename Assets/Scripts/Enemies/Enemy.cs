using UnityEngine;

public enum EnemyMovementType { Straight, ZigZag }

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int damageToCore = 10;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int scoreValue = 10;

    [Header("Strategy")]
    [SerializeField] private EnemyMovementType movementType = EnemyMovementType.Straight;

    private IMovementStrategy movementStrategy;
    private Transform target;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public bool IsAlive => CurrentHealth > 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        movementStrategy = CreateStrategy(movementType);
    }

    
    public void Initialize(Transform coreTransform, EnemyMovementType type)
    {
        target = coreTransform;
        movementType = type;
        movementStrategy = CreateStrategy(type);
        CurrentHealth = maxHealth;
    }

    private IMovementStrategy CreateStrategy(EnemyMovementType type)
    {
        
        return type switch
        {
            EnemyMovementType.ZigZag => new ZigZagMovement(),
            _ => new StraightMovement(),
        };
    }

    private void Update()
    {
        if (!IsAlive || target == null) return;
        movementStrategy.Move(transform, target, speed, Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Die();
    }

    private void Die()
    {
        GameEvents.RaiseEnemyDied(transform.position);
        GameEvents.RaiseScoreChanged(scoreValue);
        Destroy(gameObject);  
    }

    // Core ile çarpışma
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnergyCore>(out var core))
        {
            core.TakeDamage(damageToCore);
            Destroy(gameObject);
        }
    }
}