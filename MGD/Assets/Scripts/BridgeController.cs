using UnityEngine;
using System.Collections;

public class BridgeController : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float fixedDuration = 3f; // 修复后维持多久

    private void Start() {
        animator = GetComponent<Animator>();
    }

    // 桥的状态
    private enum BridgeState {
        Broken,      // 断桥，闲置中，可以被触发修复
        Recovering,  // 正在播放 Recover 动画
        Fixed,       // 已修复，维持中（倒计时中）
        Destroying   // 正在播放 Destroy 动画
    }

    private BridgeState currentState = BridgeState.Broken;

    // 提供给外部（触发区域）判断是否能修复的入口
    public bool CanRepair => currentState == BridgeState.Broken;

    public void Repair() {
        if (!CanRepair) return; // 双重保险，防止外部没检查就调用
        StartCoroutine(RepairSequence());
    }

    public void ChangeStateToFixed() {
        currentState = BridgeState.Fixed;
        StartCoroutine(WaitTimeDestroy());
    }

    private IEnumerator WaitTimeDestroy() {
        yield return new WaitForSeconds(fixedDuration);
        DestroyState();
    }


    private void DestroyState() {
        currentState = BridgeState.Destroying;
        animator.SetBool("Recover", false);
        animator.SetBool("Destroy", true);
    }

    public void BrokenState() {
        currentState = BridgeState.Broken;
    }

    private IEnumerator RepairSequence() {
        currentState = BridgeState.Recovering;
        animator.SetBool("Destroy", false);
        animator.SetBool("Recover", true);

        // 等 Recover 动画播完
        yield return WaitForAnimation("Recover");

        currentState = BridgeState.Fixed;

        // 维持一段时间（桥是好的，可以走）
        yield return new WaitForSeconds(fixedDuration);

        currentState = BridgeState.Destroying;
        animator.SetBool("Recover", false);
        animator.SetBool("Destroy", true);

        // 等 Destroy 动画播完
        yield return WaitForAnimation("Destroy");

        currentState = BridgeState.Broken; // 回到可被再次修复的状态
    }

    // 等待当前 Animator 播放完指定动画（不依赖固定秒数，更稳）
    private IEnumerator WaitForAnimation(string stateName) {
        // 等一帧，确保 Animator 已经切换到目标状态
        yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)
               && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) {
            yield return null;
        }
    }
}