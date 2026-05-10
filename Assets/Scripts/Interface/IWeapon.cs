using UnityEngine;

public interface IWeapon
{
    void Fire(Vector2 origin, Vector2 direction);
    int Damage { get; }
    float FireRate { get; }      // shoot per second
}