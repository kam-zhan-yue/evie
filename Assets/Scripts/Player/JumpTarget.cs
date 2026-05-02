using UnityEngine;

public class JumpTarget : MonoBehaviour
{
  private JumpTargetPopup _popup;

  private void Awake()
  {
    _popup = GetComponentInChildren<JumpTargetPopup>();
  }

  private void Start()
  {
    SetActive(false);
  }

  public void SetActive(bool active)
  {
    if (active)
      _popup.ShowPopup();
    else
      _popup.HidePopup();
  }
}
