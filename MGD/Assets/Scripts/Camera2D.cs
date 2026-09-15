using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Camera2D : MonoBehaviour {
    public static Camera2D instance;

    [Header("Follow Toggle")]
    [SerializeField] public bool horizontalFollow = true;
    [SerializeField] public bool verticalFollow = true;
    public bool stopFollow = false;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = -3f;
    [SerializeField] private float horizontalSmoothness = 3f;

    [Header("Vertical")]
    [SerializeField][Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    [Header("Camera Bounds")]
    [SerializeField] private bool useBounds = true;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 100f;

    [Header("Offset Transition")]
    [SerializeField] private float offsetTransitionSpeed = 2f;
    [SerializeField] private float _targetHorizontalOffset;
    [SerializeField] private float _targetVerticalOffset;

    private float _targetHorizontalSmoothFollow;
    private float _targetVerticalSmoothFollow;

    public GameObject Target;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        _targetHorizontalOffset = horizontalOffset;
        _targetVerticalOffset = verticalOffset;
    }

    private void Update() {
        MoveCamera();
    }


    private void MoveCamera() {
        if (Target == null) return;
        if (stopFollow) return;

        horizontalOffset = Mathf.Lerp(horizontalOffset, _targetHorizontalOffset, offsetTransitionSpeed * Time.deltaTime);
        verticalOffset = Mathf.Lerp(verticalOffset, _targetVerticalOffset, offsetTransitionSpeed * Time.deltaTime);

        Vector3 targetPos = GetTargetPosition(Target);

        _targetHorizontalSmoothFollow = Mathf.Lerp(
            _targetHorizontalSmoothFollow, targetPos.x,
            horizontalSmoothness * Time.deltaTime);
        _targetVerticalSmoothFollow = Mathf.Lerp(
            _targetVerticalSmoothFollow, targetPos.y,
            verticalSmoothness * Time.deltaTime);

        float xPos = horizontalFollow ? _targetHorizontalSmoothFollow : transform.localPosition.x;
        float yPos = verticalFollow ? _targetVerticalSmoothFollow : transform.localPosition.y;

        if (useBounds) {
            xPos = Mathf.Clamp(xPos, minX, maxX);
            yPos = Mathf.Clamp(yPos, minY, maxY);
        }

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }

    private Vector3 GetTargetPosition(GameObject player) {
        float xPos = (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        float yPos = (player.transform.position.y + verticalOffset) * verticalInfluence;
        return new Vector3(xPos, yPos, transform.position.z);
    }

    /*
    private void CenterOnTarget(GameObject player) {
        Target = player;

        Vector3 targetPos = GetTargetPosition(Target);
        _targetHorizontalSmoothFollow = targetPos.x;
        _targetVerticalSmoothFollow = targetPos.y;

        transform.localPosition = targetPos;
    }
    

    public void SetTargetSmooth(GameObject player) {
        Target = player;

        _targetHorizontalSmoothFollow = transform.localPosition.x;
        _targetVerticalSmoothFollow = transform.localPosition.y;
    }
    */

    public void SetOffsets(float newHorizontal, float newVertical, float speed, float newMinY, bool newIsStopFollowing) {
        _targetHorizontalOffset = newHorizontal;
        _targetVerticalOffset = newVertical;
        offsetTransitionSpeed = speed;
        minY = newMinY;
        stopFollow = newIsStopFollowing;
    }
}
