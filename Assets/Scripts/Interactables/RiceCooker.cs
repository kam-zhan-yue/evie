using Kuroneko.UtilityDelivery;
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
    ServiceLocator.Instance.Get<IInteractionService>().OnRiceCookerOpen();
    // TODO: Make this way more readable
    ServiceLocator.Instance.Get<UI>().PlayDialogue(interactor.transform.position,
      new DialogueUIData() {
        speaker = "Evie",
        color = Color.black,
        text = "Hmmm ricey rice rice",
      }
    );
  }
}
