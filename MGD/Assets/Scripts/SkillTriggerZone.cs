using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTriggerZone : MonoBehaviour {
    private enum BridgeType { Short, Long, Special }

    [SerializeField] private BridgeType bridgeType;

    [SerializeField] private BridgeController targetBridge;
    [SerializeField] private BridgeMultiController targetLongBridge;
    [SerializeField] private SpecialBridgeController targetSpecialBridge;

    private InputAction m_Skill;
    private bool isPlayerInRange = false;

    private void Awake() {
        m_Skill = InputSystem.actions.FindAction("Skill");
    }

    private void Update() {
        if (!isPlayerInRange || !m_Skill.WasPressedThisFrame()) return;

        switch (bridgeType) {
            case BridgeType.Short:
                if (targetBridge.CanRepair) targetBridge.Repair();
                break;

            case BridgeType.Long:
                if (targetLongBridge.CanActivate) targetLongBridge.Activate();
                break;

            case BridgeType.Special:
                targetSpecialBridge.ForceRepair(); // 不检查状态，永远可以触发
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) isPlayerInRange = false;
    }
}