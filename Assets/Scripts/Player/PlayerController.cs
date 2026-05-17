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
        Debug.Log("Update CALISIYOR");
        if (Input.anyKey)
        {
            Debug.Log("Bir tusa basildi!");
        }
        HandleMovement();
        HandleFire();
    }

    private void HandleMovement()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  y = -1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    y = 1f;

        Vector2 input = new Vector2(x, y).normalized;
        rb.linearVelocity = new Vector2(x, y).normalized * 5f;
        Debug.Log($"KeyInput: X={x} Y={y}");
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