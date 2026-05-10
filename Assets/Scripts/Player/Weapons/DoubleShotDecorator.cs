using UnityEngine;

public class DoubleShotDecorator : WeaponDecorator
{
    private const float AngleOffset = 8f;  // derece

    public DoubleShotDecorator(IWeapon inner) : base(inner) { }

    public override void Fire(Vector2 origin, Vector2 direction)
    {
        Vector2 left  = Rotate(direction, +AngleOffset);
        Vector2 right = Rotate(direction, -AngleOffset);
        inner.Fire(origin, left);
        inner.Fire(origin, right);
    }

    private static Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}