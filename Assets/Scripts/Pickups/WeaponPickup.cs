using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponUpgradeType upgradeType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var pc))
        {
            
            Debug.Log($"Pickup alındı: {upgradeType} upgrade uygulanıyor");
            pc.UpgradeWeapon(upgradeType);
            Destroy(gameObject);
        }
    }
}