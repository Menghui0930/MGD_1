using UnityEngine;

public class MazeDropController : MonoBehaviour {
    [SerializeField] private Transform maze;

    [SerializeField] private Transform mazeStartPoint;
    [SerializeField] private Transform mazeEndPoint;

    [SerializeField] private float moveSpeed = 2f;

    private bool isActive = false;   
    private bool hasFinished = false; 

    private Vector3 lastPosition;
    public Vector3 DeltaMovement { get; private set; } 

    private void Start() {
        ResetMaze();
    }

    private void Update() {
        if (!isActive || hasFinished) {
            DeltaMovement = Vector3.zero;
            return;
        }

        maze.position = Vector3.MoveTowards(maze.position, mazeEndPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(maze.position, mazeEndPoint.position) < 0.05f) {
            maze.position = mazeEndPoint.position;
            isActive = false;
            hasFinished = true;
        }

        DeltaMovement = maze.position - lastPosition;
        lastPosition = maze.position;
    }

    public void StartDropping() {
        if (!hasFinished && !isActive) {
            isActive = true;
        }
    }

    public void ResetMaze() {
        isActive = false;
        hasFinished = false;
        maze.position = mazeStartPoint.position;
        lastPosition = maze.position;
        DeltaMovement = Vector3.zero;
    }
}