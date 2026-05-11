using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 5;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public bool IsAlive => CurrentHealth > 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        GameEvents.RaisePlayerHealthChanged(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        GameEvents.RaisePlayerHealthChanged(CurrentHealth, MaxHealth);
    }
}