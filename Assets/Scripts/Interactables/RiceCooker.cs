using UnityEngine;
using DG.Tweening;

public class RiceCooker : Interactable
{
  [SerializeField] private Transform hinge;

  [SerializeField] private Vector3 hingeAngle = new(0, 0, 90);
  [SerializeField] private float animationTime = 0.8f;

  public override void Activate(Interactor interactor)
  {
    base.Activate(interactor);
    // Animate the lid here
    animating = true;
    hinge.DORotate(hingeAngle, animationTime)
      .SetEase(Ease.OutExpo)
      .OnComplete(() => {
        animating = false;
    });
  }
}
