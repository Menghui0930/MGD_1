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
            platformCollider.enabled = true; // 还没完全展开前先关闭
        }
        RestartRoutine(maxScale, growDuration, onComplete: () => {
            // 完全展开后才能站
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
            platformCollider.enabled = false; // 立刻不能站
        }
        RestartRoutine(0.3804588f, shrinkDuration, onComplete: null);
    }

    private void RestartRoutine(float targetScale, float duration, System.Action onComplete) {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ScaleTo(targetScale, duration, onComplete));
    }

    private IEnumerator ScaleTo(float targetScale, float duration, System.Action onComplete) {
        float startScale = circleMask.localScale.x; // 从当前实际大小开始，不会跳变
        float elapsed = 0f;

        // 根据剩余距离等比例调整时长，这样不管从哪个进度开始，速度感觉是一致的
        float distance = Mathf.Abs(targetScale - startScale);
        float fullDistance = Mathf.Max(maxScale, 0.001f);
        float adjustedDuration = duration * (distance / fullDistance);
        if (adjustedDuration < 0.01f) adjustedDuration = 0.01f;

        while (elapsed < adjustedDuration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / adjustedDuration);
            t = t * t * (3f - 2f * t); // smoothstep 缓动
            float current = Mathf.Lerp(startScale, targetScale, t);
            circleMask.localScale = new Vector3(current, current, 1f);
            yield return null;
        }

        circleMask.localScale = new Vector3(targetScale, targetScale, 1f);
        onComplete?.Invoke();
    }
}