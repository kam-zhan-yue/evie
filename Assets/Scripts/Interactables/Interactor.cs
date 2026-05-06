using UnityEngine;
using System.Collections.Generic;
using ZLinq;

public class Interactor : MonoBehaviour
{
  private List<Interactable> _interactables = new();

  private void OnTriggerEnter(Collider other) 
  {
    if (TryFindInteractable(other, out Interactable interactable))
      Add(interactable);
  }

  private void OnTriggerExit(Collider other)
  {
    if (TryFindInteractable(other, out Interactable interactable))
      Remove(interactable);
  }

  private bool TryFindInteractable(Collider collider, out Interactable interactable)
  {
    if (collider.TryGetComponent(out interactable))
      return true;
    if (collider.TryGetComponent(out InteractableChild child))
    {
      interactable = child.Interactable;
      return true;
    }
    return false;
  }

  private void Add(Interactable interactable)
  {
    interactable.OnDestroyed += Remove;
    if (!_interactables.Contains(interactable))
      _interactables.Add(interactable);
  }

  private void Remove(Interactable interactable)
  {
    interactable.SetActive(false);
    interactable.OnDestroyed -= Remove;
    _interactables.Remove(interactable);
  }

  private void Update()
  {
    foreach (Interactable interactable in _interactables)
    {
      interactable.SetActive(false);
    }
    if (TryGetClosest(out Interactable closest))
    {
      closest.SetActive(true);
    }
  }
   
  public bool TryGetClosest(out Interactable target) 
  {
    target = _interactables.AsValueEnumerable()
      .Where(interactable => interactable.CanInteract())
      .MinBy(target => GetDistance(target));
    return target != null;
  }

  private float GetDistance(Interactable target) => Vector3.Distance(target.transform.position, transform.position);
}
