using UnityEngine;
using Kuroneko.UtilityDelivery;

public abstract class Popup : MonoBehaviour
{
  private RectTransform _mainHolder;

  protected virtual void Awake()
  {
    _mainHolder = transform.GetChild(0).GetComponent<RectTransform>();
  }

  public virtual void ShowPopup()
  {
    _mainHolder.gameObject.SetActiveFast(true);
  }

  public virtual void HidePopup() 
  {
    _mainHolder.gameObject.SetActiveFast(false);
  }
}
