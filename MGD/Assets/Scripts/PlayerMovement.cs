using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private InputAction m_Move;
    private InputAction m_Jump;

    #region Touch Input

    private float movetouchInput;
    private bool touchJumpPressed = false;
    public void OnleftButtonDown() => movetouchInput = -1f;
    public void OnRightButtonDown() => movetouchInput = 1f;
    public void OnMoveButtonUp() => movetouchInput = 0f;
    public void OnJumpButtonDown() => touchJumpPressed = true;

    #endregion

    #region Move

    private Vector2 moveDir;
    private float h_Input;
    [SerializeField] private float moveSpeed = 10f;
    private float _movement;

    #endregion

    #region Jump
    public bool isJumping = false;
    public Transform groundCheckerBottom; 
    public Transform groundCheckerTop;    
    public bool isGrounded = false;
    public LayerMask groundMask;

    private float jumpForce;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;
    public int JumpLeft;

    #endregion

    [SerializeField] private float fallMultiplier = 1.5f;
    private Rigidbody2D theRB;

    [Header("Gravity Flip")]
    [SerializeField] private float baseGravityScale = 3f; // Original RB2D Gravity Scale Value
    [SerializeField] private float flipThreshold = 0.6f;  // Acceleration threshold when triggers flip
    private int gravityDir = 1; // 1 = normal, -1 = flip
    private Vector3 smoothedAccel;

    private void Awake() {
        m_Move = InputSystem.actions.FindAction("Move");
        m_Jump = InputSystem.actions.FindAction("Jump");

        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    void Start() {
        theRB = GetComponent<Rigidbody2D>();
        JumpLeft = maxJumps;
        theRB.gravityScale = baseGravityScale;
    }

    void Update() {
        CheckDeviceFlip();

        // move
        moveDir = m_Move.ReadValue<Vector2>();
        h_Input = moveDir.x;
        float finalInput = Mathf.Abs(movetouchInput) > 0.01f ? movetouchInput : h_Input;
        _movement = Mathf.Abs(finalInput) > 0.1f ? finalInput : 0f;

        // Select which ground Checker to use based on the current direction of gravity.
        Transform activeChecker = gravityDir == 1 ? groundCheckerBottom : groundCheckerTop;
        isGrounded = Physics2D.OverlapCircle(activeChecker.position, 0.3f, groundMask);

        if (isGrounded && jumpForce == 0f && theRB.linearVelocityY == 0f) {
            JumpLeft = maxJumps;
            isJumping = false;
        }

        theRB.linearVelocity = new Vector2(_movement * moveSpeed, theRB.linearVelocityY);

        if (m_Jump.WasPressedThisFrame() || touchJumpPressed) {
            Jump();
            touchJumpPressed = false;
        }

        if (isJumping) {
            theRB.linearVelocityY = 0f;
            theRB.AddForce(new Vector2(0, jumpForce * gravityDir), ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = false;
            jumpForce = 0f;
        }

        // 下落加速：判断"是否正在朝重力方向运动"
        bool isFalling = gravityDir == 1
            ? theRB.linearVelocity.y < 0
            : theRB.linearVelocity.y > 0;

        if (isFalling) {
            theRB.linearVelocity += Vector2.up * Physics2D.gravity.y * gravityDir
                                     * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Jump() {
        if (JumpLeft == 0) return;
        if (isGrounded || JumpLeft > 0) {
            JumpLeft -= 1;
            jumpForce = Mathf.Sqrt(jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            isJumping = true;
        }
    }

    private void CheckDeviceFlip() {
        if (Accelerometer.current == null) return;

        Vector3 raw = Accelerometer.current.acceleration.ReadValue();
        smoothedAccel = Vector3.Lerp(smoothedAccel, raw, 5f * Time.deltaTime);

        if (gravityDir == 1 && smoothedAccel.y > flipThreshold) {
            SetGravityDir(-1);
        } else if (gravityDir == -1 && smoothedAccel.y < -flipThreshold) {
            SetGravityDir(1);
        }
    }

    private void SetGravityDir(int dir) {
        gravityDir = dir;
        theRB.gravityScale = baseGravityScale * gravityDir;

        // flip player

    }

    private void OnDrawGizmos() {
        if (groundCheckerBottom != null) {
            Gizmos.color = (gravityDir == 1 && isGrounded) ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheckerBottom.position, 0.3f);
        }
        if (groundCheckerTop != null) {
            Gizmos.color = (gravityDir == -1 && isGrounded) ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheckerTop.position, 0.3f);
        }
    }
}