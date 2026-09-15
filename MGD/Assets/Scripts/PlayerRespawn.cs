using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    private Rigidbody2D theRB;

    private void Awake() {
        theRB = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        // 游戏开始时，把玩家当前位置设为初始checkpoint
        CheckpointManager.Instance.SetInitialCheckpoint(transform.position);
    }

    public void Die() {
        Vector3 respawnPos = CheckpointManager.Instance.GetCheckpointPos();

        // 传送回checkpoint
        transform.position = respawnPos;

        // 清空速度，避免带着摔死前的冲力复活
        if (theRB != null) {
            theRB.linearVelocity = Vector2.zero;
        }

        // 这里以后可以加：播放死亡动画、扣血、播放特效、短暂无敌时间等
    }
}