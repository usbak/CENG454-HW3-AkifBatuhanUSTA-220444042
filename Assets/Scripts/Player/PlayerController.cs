using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Weapon")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private float baseFireRate = 5f;

    private Rigidbody2D rb;
    private Camera cam;
    private IWeapon weapon;
    private float nextFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        weapon = new BaseWeapon(baseDamage, baseFireRate);
    }

    private void Update()
    {
        HandleMovement();
        HandleFire();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(x, y).normalized;
        rb.linearVelocity = input * moveSpeed;
    }

    private void HandleFire()
    {
        if (!Input.GetMouseButton(0)) return;
        if (Time.time < nextFireTime) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = ((Vector2)(mouseWorld - firePoint.position)).normalized;
        weapon.Fire(firePoint.position, dir);

        nextFireTime = Time.time + 1f / weapon.FireRate;
    }

    
    public void UpgradeWeapon(WeaponUpgradeType type)
    {
        weapon = type switch
        {
            WeaponUpgradeType.DoubleShot => new DoubleShotDecorator(weapon),
            WeaponUpgradeType.Piercing => new PiercingDecorator(weapon),
            _ => weapon
        };
        GameEvents.RaiseWeaponUpgraded(type.ToString());
    }
}

public enum WeaponUpgradeType { DoubleShot, Piercing }