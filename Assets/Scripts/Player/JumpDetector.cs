using System.Collections.Generic;
using UnityEngine;
using ZLinq;

public class JumpDetector : MonoBehaviour
{
  private List<JumpTarget> _targets = new();

  private void OnTriggerEnter(Collider other) 
  {
    if (other.TryGetComponent(out JumpTarget target)) 
    {
      _targets.Add(target);
    }
  }

  private void OnTriggerExit(Collider other) 
  {
    if (other.TryGetComponent(out JumpTarget target)) 
    {
      target.SetActive(false);
      _targets.Remove(target);
    }
  }

  private void Update()
  {
    foreach (JumpTarget target in _targets) 
    {
      target.SetActive(false);
    }
    if (TryGetClosest(out JumpTarget closest)) 
    {
      closest.SetActive(true);
    }
  }

  public bool TryGetClosest(out JumpTarget target) 
  {
    target = _targets.AsValueEnumerable()
      .Where(target => GetVerticalDistance(target) > 0.2f)
      .MinBy(target => GetDistance(target));
    return target != null;
  }

  private float GetDistance(JumpTarget target) => Vector3.Distance(target.transform.position, transform.position);
  private float GetVerticalDistance(JumpTarget target) => Mathf.Abs(target.transform.position.y - transform.position.y);
}
