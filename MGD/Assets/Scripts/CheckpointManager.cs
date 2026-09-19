using UnityEngine;

public class CheckpointManager : MonoBehaviour {
    public static CheckpointManager Instance { get; private set; }

    private Vector3 currentCheckpointPos;
    private bool hasCheckpoint = false;

    private void Awake() {
        // 简单单例模式
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 玩家出生时调用一次，把出生点设为第一个checkpoint
    public void SetInitialCheckpoint(Vector3 pos) {
        if (!hasCheckpoint) {
            currentCheckpointPos = pos;
            hasCheckpoint = true;
        }
    }

    // 碰到新checkpoint时更新
    public void SetCheckpoint(Vector3 pos) {
        currentCheckpointPos = pos;
        hasCheckpoint = true;
    }

    public Vector3 GetCheckpointPos() {
        return currentCheckpointPos;
    }
}