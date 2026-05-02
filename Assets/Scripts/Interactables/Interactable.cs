using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
  // Exposed Variables
  [SerializeField] private readonly bool activateOnce = true;

  // Components
  private InteractablePopup _popup;

  // Private Variables
  private bool _activated = false;
  protected bool animating = false;

  protected virtual void Awake()
  {
    _popup = GetComponentInChildren<InteractablePopup>();
  }

  protected virtual void Start()
  {
    SetActive(false);
  }
  
  public virtual void Activate()
  {
    _activated = true;
  }

  public virtual bool CanInteract()
  {
    return activateOnce && !_activated && !animating;
  }

  public void SetActive(bool active)
  {
    if (active)
      ShowActive();
    else
      HideActive();
  }

  protected virtual void ShowActive()
  {
      _popup.ShowPopup();
  }

  protected virtual void HideActive()
  {
      _popup.HidePopup();
  }
}
