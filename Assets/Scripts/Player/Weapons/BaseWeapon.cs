using UnityEngine;


public class BaseWeapon : IWeapon
{
    public int Damage { get; }
    public float FireRate { get; }

    public BaseWeapon(int damage = 1, float fireRate = 5f)
    {
        Damage = damage;
        FireRate = fireRate;
    }

    public virtual void Fire(Vector2 origin, Vector2 direction)
    {
        Bullet b = PoolManager.Instance.BulletPool.Get(origin, Quaternion.identity);
        b.Launch(direction, Damage, isPiercing: false);
    }
}