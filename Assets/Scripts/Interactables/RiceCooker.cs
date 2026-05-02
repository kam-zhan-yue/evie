using UnityEngine;
using DG.Tweening;

public class RiceCooker : Interactable
{
  [SerializeField] private readonly Transform hinge;

  [SerializeField] private Vector3 hingeAngle = new(0, 0, 90);
  [SerializeField] private readonly float animationTime = 0.8f;

  public override void Activate()
  {
    base.Activate();
    // Animate the lid here
    animating = true;
    hinge.DORotate(hingeAngle, animationTime)
      .SetEase(Ease.OutExpo)
      .OnComplete(() => {
        animating = false;
    });
  }
}
