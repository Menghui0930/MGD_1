using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTriggerZone : MonoBehaviour {
    [SerializeField] private BridgeController targetBridge;
    [SerializeField] private BridgeMultiController targetLongBridge;

    public bool shortBridge = true;
    public bool LongBridge = false;

    private InputAction m_Skill;
    private bool isPlayerInRange = false;

    private void Awake() {
        m_Skill = InputSystem.actions.FindAction("Skill");
    }

    private void Update() {
        if (isPlayerInRange && m_Skill.WasPressedThisFrame()) {
            if (targetBridge.CanRepair) {
                if (shortBridge) {
                    targetBridge.Repair();
                } else if (LongBridge) {
                    targetLongBridge.Activate();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) isPlayerInRange = false;
    }
}