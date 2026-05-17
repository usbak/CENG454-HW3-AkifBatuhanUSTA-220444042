using UnityEngine;

public enum EnemyMovementType { Straight, ZigZag }

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")] [SerializeField] private int maxHealth = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int scoreValue = 10;

    [Header("Strategy")] [SerializeField] private EnemyMovementType movementType = EnemyMovementType.Straight;

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


    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.GetComponent<Bullet>() != null) return;

        
        if (other.TryGetComponent<IDamageable>(out var target) && target.IsAlive)
        {
            target.TakeDamage(damage);

            
            Die();
        }
    }

    
}