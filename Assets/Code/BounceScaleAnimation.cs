using DG.Tweening;
using UnityEngine;

public class BounceScaleAnimation : MonoBehaviour
{
    [Header("Scale")] [SerializeField] private Vector3 startScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);

    [Header("Timing")] [SerializeField] private float startDelay;
    [SerializeField] private float toEndMoveTime = 0.25f;
    [SerializeField] private float toStartMoveTime = 0.25f;

    [Header("Eases")] [SerializeField] private Ease toEndEase = Ease.Linear;
    [SerializeField] private Ease toStartEase = Ease.Linear;

    public void Activate() =>
        ActivateScaleBouncing();

    private void ActivateScaleBouncing()
    {
        gameObject.transform.localScale = startScale;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startDelay);
        sequence.Append(transform.DOScale(endScale, toEndMoveTime).SetEase(toEndEase));
        sequence.Append(transform.DOScale(startScale, toStartMoveTime).SetEase(toStartEase));
    }
}