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

    // 全局广播事件，任何脚本调用 SkillTriggerZone.RaiseSkillButtonPressed() 都会通知所有订阅者
    public static event System.Action OnSkillButtonPressed;

    private void Awake() {
        m_Skill = InputSystem.actions.FindAction("Skill");
    }

    private void OnEnable() {
        OnSkillButtonPressed += HandleSkillButtonPressed;
    }

    private void OnDisable() {
        OnSkillButtonPressed -= HandleSkillButtonPressed;
    }

    private void Update() {
        if (isPlayerInRange && m_Skill.WasPressedThisFrame()) {
            TriggerSkill();
        }
    }

    private void HandleSkillButtonPressed() {
        if (isPlayerInRange) {
            TriggerSkill();
        }
    }

    public static void RaiseSkillButtonPressed() {
        OnSkillButtonPressed?.Invoke();
    }

    private void TriggerSkill() {
        switch (bridgeType) {
            case BridgeType.Short:
                if (targetBridge.CanRepair) targetBridge.Repair();
                break;

            case BridgeType.Long:
                if (targetLongBridge.CanActivate) targetLongBridge.Activate();
                break;

            case BridgeType.Special:
                targetSpecialBridge.ForceRepair();
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