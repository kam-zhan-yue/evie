using UnityEngine;

public class Computer : MonoBehaviour
{
  [SerializeField] private float typingSpeed = 10f;
  private ComputerPopup _popup;
  private Keyboard _keyboard;

  private void Awake()
  {
    _keyboard = GetComponentInChildren<Keyboard>();
    _popup = GetComponentInChildren<ComputerPopup>();
  }

  private void Start()
  {
    _keyboard.Init(_popup, typingSpeed);
  }
}
