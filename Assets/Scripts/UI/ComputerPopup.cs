using UnityEngine;
using TMPro;

public class ComputerPopup : Popup
{
  [SerializeField] private TMP_Text screenText;
  [SerializeField] private int maxChars = 100;

  protected override void Awake()
  {
    base.Awake();
    screenText.SetText(string.Empty);
  }

  public void TypeCharacter(char c)
  {
    // TODO: Play animation here?
    if (screenText.text.Length >= maxChars) return;
    screenText.SetText(screenText.text + c);
  }
}
