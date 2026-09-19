using UnityEngine;

public class CameraZone : MonoBehaviour {
    [Header("Camera Offset Override")]
    [SerializeField] private bool SetVerticalOffset;
    [SerializeField] private float horizontalOffset = -3f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float transitionSpeed = 2f;

    [Header("SetY")]
    [SerializeField] private bool SetY;
    [SerializeField] private float MinY = 0.85f;  
    [SerializeField] private float MaxY = 0.85f;

    [Header("SetX")]
    [SerializeField] private bool SetX;
    [SerializeField] private float MinX = 0.85f;
    [SerializeField] private float MaxX = 0.85f;

    [SerializeField] private bool isStopfollowing = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (SetY) {
                Camera2D.instance.SetMinY(MinY, MaxY);
            }

            if (SetX) {
                Camera2D.instance.SetMinX(MinX, MaxX);
            }

            if (SetVerticalOffset) {
                Camera2D.instance.SetVerticalOffset(verticalOffset);
            }
        }
    }
}