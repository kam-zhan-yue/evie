using UnityEngine;
using DG.Tweening;

public class RiceCooker : Interactable
{
  [SerializeField] private Transform hinge;

  [SerializeField] private Vector3 hingeAngle = new(0, 0, 90);
  [SerializeField] private float animationTime = 0.8f;

  public override void Activate()
  {
    base.Activate();
    // Animate the lid here
    animating = true;
    hinge.DORotate(new Vector3(0, 0, 90), animationTime)
      .SetEase(Ease.OutExpo)
      .OnComplete(() => {
        animating = false;
    });
  }
}
