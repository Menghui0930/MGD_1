using UnityEngine;
using System.Collections;

public class BridgeMultiController : MonoBehaviour {
    [System.Serializable]
    public class BridgeGroup {
        [Tooltip("这一组包含的桥节，按顺序拖入")]
        public Transform[] segments;

        [Tooltip("这一组相对技能触发那一刻，延迟多少秒才开始上升")]
        public float startDelay = 0f;
    }

    [Header("按组编排的桥")]
    [SerializeField] private BridgeGroup[] groups;

    [Header("时间参数")]
    [SerializeField] private float riseDuration = 2f;
    [SerializeField] private float stayDuration = 1.5f;
    [SerializeField] private float fallDuration = 2f;
    [SerializeField] private float staggerDelay = 0.2f; // 组内每节之间的间隔

    [Header("起点偏移（相对设计好的位置往下多少）")]
    [SerializeField] private float downOffsetY = -5f;

    // 每节桥各自的上/下位置，用二维结构对应 groups[i].segments[j]
    private Vector3[][] upLocalPos;
    private Vector3[][] downLocalPos;

    private bool isActive = false;
    public bool CanActivate => !isActive;

    private void Awake() {
        upLocalPos = new Vector3[groups.Length][];
        downLocalPos = new Vector3[groups.Length][];

        for (int g = 0; g < groups.Length; g++) {
            var segs = groups[g].segments;
            upLocalPos[g] = new Vector3[segs.Length];
            downLocalPos[g] = new Vector3[segs.Length];

            for (int i = 0; i < segs.Length; i++) {
                upLocalPos[g][i] = segs[i].localPosition;
                downLocalPos[g][i] = upLocalPos[g][i] + new Vector3(0, downOffsetY, 0);
                segs[i].localPosition = downLocalPos[g][i];
            }
        }
    }

    public void Activate() {
        if (!CanActivate) return;
        StartCoroutine(ActivateSequence());
    }

    private IEnumerator ActivateSequence() {
        isActive = true;

        float maxFinishTime = 0f;

        for (int g = 0; g < groups.Length; g++) {
            StartCoroutine(RunGroup(g));

            float groupFinishTime = groups[g].startDelay
                                     + (groups[g].segments.Length - 1) * staggerDelay
                                     + riseDuration + stayDuration + fallDuration;

            if (groupFinishTime > maxFinishTime)
                maxFinishTime = groupFinishTime;
        }

        // 等到所有组都完全跑完（回到最初的下方位置）
        yield return new WaitForSeconds(maxFinishTime);

        isActive = false;
    }

    // 单一组：等待自己的 startDelay，再按 staggerDelay 依次启动组内每一节
    private IEnumerator RunGroup(int groupIndex) {
        var group = groups[groupIndex];

        yield return new WaitForSeconds(group.startDelay);

        for (int i = 0; i < group.segments.Length; i++) {
            StartCoroutine(SegmentLifeCycle(groupIndex, i));
            yield return new WaitForSeconds(staggerDelay);
        }
    }

    private IEnumerator SegmentLifeCycle(int groupIndex, int segIndex) {
        Transform segment = groups[groupIndex].segments[segIndex];
        Vector3 down = downLocalPos[groupIndex][segIndex];
        Vector3 up = upLocalPos[groupIndex][segIndex];

        yield return MoveSegment(segment, down, up, riseDuration);
        yield return new WaitForSeconds(stayDuration);
        yield return MoveSegment(segment, up, down, fallDuration);
    }

    private IEnumerator MoveSegment(Transform segment, Vector3 from, Vector3 to, float duration) {
        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = t * t * (3f - 2f * t);
            segment.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
        segment.localPosition = to;
    }
}