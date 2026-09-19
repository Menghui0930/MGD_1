using UnityEngine;

public class MazeStartTrigger : MonoBehaviour {
    [SerializeField] private MazeDropController maze;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            maze.StartDropping();
        }
    }
}