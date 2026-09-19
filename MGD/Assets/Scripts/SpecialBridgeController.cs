using UnityEngine;
using System.Collections;

public class SpecialBridgeController : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float fixedDuration = 3f;

    private enum BridgeState { Broken, Recovering, Fixed, Destroying }
    private BridgeState currentState = BridgeState.Broken;

    private Coroutine currentRoutine;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    // 不检查任何状态，随时可以被调用，强制打断重来
    public void ForceRepair() {
        if (currentRoutine != null) {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(ForceRepairSequence());
    }

    private IEnumerator ForceRepairSequence() {
        // 无论现在在哪个状态，强制打断，先跳到 Destroy
        currentState = BridgeState.Destroying;
        animator.SetBool("Recover", false);
        animator.SetBool("Destroy", true);

        // 等一帧，确保 Animator 真的处理了这次跳转（Any State -> Destroy）
        yield return null;

        // 立刻接上 Recover
        currentState = BridgeState.Recovering;
        animator.SetBool("Destroy", false);
        animator.SetBool("Recover", true);

        yield return WaitForAnimation("Recover");

        currentState = BridgeState.Fixed;
        yield return new WaitForSeconds(fixedDuration);

        currentState = BridgeState.Destroying;
        animator.SetBool("Recover", false);
        animator.SetBool("Destroy", true);

        yield return WaitForAnimation("Destroy");

        currentState = BridgeState.Broken;
    }

    private IEnumerator WaitForAnimation(string stateName) {
        yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)
               && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) {
            yield return null;
        }
    }
}