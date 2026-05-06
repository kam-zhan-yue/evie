using UnityEngine;

public class InteractableChild : MonoBehaviour
{
  private Interactable _interactable;
  public Interactable Interactable => _interactable;

  public void Init(Interactable interactable)
  {
    _interactable = interactable;
  }
}
