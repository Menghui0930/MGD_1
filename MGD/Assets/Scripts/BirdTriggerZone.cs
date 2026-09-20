using System.Collections;
using UnityEngine;

public class BirdTriggerZone : MonoBehaviour {
    [SerializeField] private Transform circleMask;

    [SerializeField] private EdgeCollider2D platformCollider;

    [SerializeField] private float maxScale = 3f;
    [SerializeField] private float growDuration = 1f;
    [SerializeField] private float shrinkDuration = 0.6f;

    private Coroutine currentRoutine;
    private bool isGrown = false; 

    private void Start() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Grow();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Shrink();
        }
    }

    public void Grow() {
        if (isGrown) return;
        isGrown = true;

        if (platformCollider != null) {
            platformCollider.enabled = true; 
        }
        RestartRoutine(maxScale, growDuration, onComplete: () => {
            if (isGrown) {
                if (platformCollider != null) {
                    platformCollider.enabled = true;
                }
            }
        });
    }

    public void Shrink() {
        if (!isGrown) return;
        isGrown = false;

        if (platformCollider != null) {
            platformCollider.enabled = false; 
        }
        RestartRoutine(0.3804588f, shrinkDuration, onComplete: null);
    }

    private void RestartRoutine(float targetScale, float duration, System.Action onComplete) {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ScaleTo(targetScale, duration, onComplete));
    }

    private IEnumerator ScaleTo(float targetScale, float duration, System.Action onComplete) {
        float startScale = circleMask.localScale.x; 
        float elapsed = 0f;

        float distance = Mathf.Abs(targetScale - startScale);
        float fullDistance = Mathf.Max(maxScale, 0.001f);
        float adjustedDuration = duration * (distance / fullDistance);
        if (adjustedDuration < 0.01f) adjustedDuration = 0.01f;

        while (elapsed < adjustedDuration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / adjustedDuration);
            t = t * t * (3f - 2f * t); 
            float current = Mathf.Lerp(startScale, targetScale, t);
            circleMask.localScale = new Vector3(current, current, 1f);
            yield return null;
        }

        circleMask.localScale = new Vector3(targetScale, targetScale, 1f);
        onComplete?.Invoke();
    }
}