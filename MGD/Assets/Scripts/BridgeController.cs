using UnityEngine;
using System.Collections;

public class BridgeController : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float fixedDuration = 3f; 

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private enum BridgeState {
        Broken,      
        Recovering,  
        Fixed,       
        Destroying   
    }

    private BridgeState currentState = BridgeState.Broken;

    public bool CanRepair => currentState == BridgeState.Broken;

    public void Repair() {
        if (!CanRepair) return; 
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