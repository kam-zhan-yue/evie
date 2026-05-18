using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour, IEventService
{
  private readonly List<IEventListener> _listeners = new();

  public void Emit(GameEvent gameEvent)
  {
    foreach (IEventListener listener in _listeners) 
    {
      listener.Emit(gameEvent);
    }
  }

  public void Register(IEventListener listener)
  {
    _listeners.Add(listener);
  }
}
