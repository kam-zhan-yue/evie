using UnityEngine;
using Kuroneko.UtilityDelivery;

public abstract class Popup : MonoBehaviour
{
  private RectTransform _mainHolder;
  protected bool _showing;

  protected virtual void Awake()
  {
    if (transform.childCount == 0) {
      Debug.LogError($"{name} is missing a MainHolder");
      enabled = false;
      return;
    }
    _mainHolder = transform.GetChild(0).GetComponent<RectTransform>();
  }

  protected virtual void Start() {
  }

  public virtual void ShowPopup()
  {
    _mainHolder.gameObject.SetActiveFast(true);
    _showing = true;
  }

  public virtual void HidePopup() 
  {
    _mainHolder.gameObject.SetActiveFast(false);
    _showing = false;
  }
}
