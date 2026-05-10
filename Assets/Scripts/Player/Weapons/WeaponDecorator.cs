using UnityEngine;


public abstract class WeaponDecorator : IWeapon
{
    protected readonly IWeapon inner;

    protected WeaponDecorator(IWeapon inner)
    {
        this.inner = inner;
    }

    public virtual int Damage => inner.Damage;
    public virtual float FireRate => inner.FireRate;
    public abstract void Fire(Vector2 origin, Vector2 direction);
}