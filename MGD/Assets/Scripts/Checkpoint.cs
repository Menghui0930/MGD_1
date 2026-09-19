using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] private bool showDebugLog = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            CheckpointManager.Instance.SetCheckpoint(transform.position);

            if (showDebugLog)
                Debug.Log("Checkpoint updated: " + transform.position);
        }
    }
}