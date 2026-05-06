using UnityEngine;

public class RiceCooker : Interactable
{
  [SerializeField] private AnimationClip openAnim;

  private Animator _animator;

  protected override void Awake()
  {
    base.Awake();
    _animator = GetComponentInChildren<Animator>();
  }

  public override void Activate(Interactor interactor)
  {
    base.Activate(interactor);
    _animator.SetTrigger("Open");
  }
}
