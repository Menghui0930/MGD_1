using UnityEngine;

public class SkillButtonUI : MonoBehaviour {
    public void OnSkillButtonClicked() {
        SkillTriggerZone.RaiseSkillButtonPressed();
    }
}