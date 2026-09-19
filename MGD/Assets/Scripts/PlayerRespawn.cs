using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    private Rigidbody2D theRB;

    [SerializeField] private MazeDropController mazesToReset;

    private void Awake() {
        theRB = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        CheckpointManager.Instance.SetInitialCheckpoint(transform.position);
    }

    public void Die() {
        Vector3 respawnPos = CheckpointManager.Instance.GetCheckpointPos();

        transform.position = respawnPos;

        if (theRB != null) {
            theRB.linearVelocity = Vector2.zero;
        }

        if (mazesToReset != null) {
            mazesToReset.ResetMaze();
        }
    }
}