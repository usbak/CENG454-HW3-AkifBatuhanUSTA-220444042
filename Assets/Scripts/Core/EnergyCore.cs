using UnityEngine;

public class EnergyCore : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public bool IsAlive => CurrentHealth > 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive || damage <= 0) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        
        GameEvents.RaiseCoreDamaged(CurrentHealth, MaxHealth);

        if (!IsAlive)
            GameEvents.RaiseCoreDestroyed();
    }
}
