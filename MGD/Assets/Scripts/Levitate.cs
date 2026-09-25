using DG.Tweening;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       transform.DOLocalMoveY(0.5f, 1f)
             .SetEase(Ease.InOutSine)
             .SetLoops(-1, LoopType.Yoyo);
    }


}
