using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float magnitude = 0.15f;
    private Vector3 originalPos;

    private void Awake() => originalPos = transform.localPosition;

    private void OnEnable() => GameEvents.OnCoreDamaged += Shake;
    private void OnDisable() => GameEvents.OnCoreDamaged -= Shake;

    private void Shake(int hp, int max) => StartCoroutine(DoShake());

    private IEnumerator DoShake()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}