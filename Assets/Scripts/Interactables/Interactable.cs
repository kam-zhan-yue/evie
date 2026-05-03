using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
  // Exposed Variables
  [SerializeField] private bool activateOnce = true;

  // Components
  private InteractablePopup _popup;

  // Private Variables
  private bool _activated = false;
  protected bool animating = false;

  public Action<Interactable> OnDestroyed;

  protected virtual void Awake()
  {
    _popup = GetComponentInChildren<InteractablePopup>();
  }

  protected virtual void Start()
  {
    SetActive(false);
  }
  
  public virtual void Activate(Interactor interactor)
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

  protected virtual void OnDestroy()
  {
    OnDestroyed?.Invoke(this);
  }
}
