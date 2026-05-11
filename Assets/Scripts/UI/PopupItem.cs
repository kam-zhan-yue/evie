using Kuroneko.UtilityDelivery;
using UnityEngine;

public abstract class PopupItem<T> : MonoBehaviour
{
  public virtual void Show() 
  {
    gameObject.SetActiveFast(true);
  }

  public virtual void Hide() 
  {
    gameObject.SetActiveFast(false);
  }

  public abstract void Init(T data);
}
