using UnityEngine;

public class CheckpointManager : MonoBehaviour {
    public static CheckpointManager Instance { get; private set; }

    private Vector3 currentCheckpointPos;
    private bool hasCheckpoint = false;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetInitialCheckpoint(Vector3 pos) {
        if (!hasCheckpoint) {
            currentCheckpointPos = pos;
            hasCheckpoint = true;
        }
    }

    public void SetCheckpoint(Vector3 pos) {
        currentCheckpointPos = pos;
        hasCheckpoint = true;
    }

    public Vector3 GetCheckpointPos() {
        return currentCheckpointPos;
    }
}