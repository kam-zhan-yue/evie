using UnityEngine;

public class Computer : Interactable
{
  [SerializeField] private float typingSpeed = 10f;
  private ComputerPopup _computerPopup;
  private Keyboard _keyboard;

  protected override void Awake()
  {
    base.Awake();
    _keyboard = GetComponentInChildren<Keyboard>();
    _computerPopup = GetComponentInChildren<ComputerPopup>();
  }

  protected override void Start()
  {
    base.Start();
    _keyboard.Init(_computerPopup, typingSpeed);
  }

  public override void Activate(Interactor interactor)
  {
    base.Activate(interactor);
    _computerPopup.HidePopup();
  }
}
