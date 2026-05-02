using UnityEngine;
using System.Collections.Generic;
using ZLinq;

public class Interactor : MonoBehaviour
{
  private List<Interactable> _interactables = new();

  private void OnTriggerEnter(Collider other) 
  {
    if (other.TryGetComponent(out Interactable interactable))
      _interactables.Add(interactable);
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.TryGetComponent(out Interactable interactable))
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
