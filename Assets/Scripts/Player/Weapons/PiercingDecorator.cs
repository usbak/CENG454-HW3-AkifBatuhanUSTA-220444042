using UnityEngine;

public class PiercingDecorator : WeaponDecorator
{
    public PiercingDecorator(IWeapon inner) : base(inner) { }

    public override void Fire(Vector2 origin, Vector2 direction)
    {
        
        Bullet b = PoolManager.Instance.BulletPool.Get(origin, Quaternion.identity);
        b.Launch(direction, inner.Damage, isPiercing: true);
    }
}