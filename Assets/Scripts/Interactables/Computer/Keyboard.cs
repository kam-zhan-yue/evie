using UnityEngine;

public class Keyboard : MonoBehaviour
{
  private Player _player;
  private ComputerPopup _popup;
  private float _typingSpeed;
  private float _timer;

  private float GetPlayerSpeed() => _player ? _player.Velocity.magnitude : 0f;

  public void Init(ComputerPopup popup, float typingSpeed)
  {
    _popup = popup;
    _typingSpeed = typingSpeed;
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.collider.TryGetComponent(out Player player))
    {
      _player = player;
      StartTyping();
    }
  }

  private void OnCollisionExit(Collision other)
  {
    if (other.collider.TryGetComponent(out Player _))
    {
      _player = null;
    }
  }

  private void StartTyping()
  {
    Type();
  }

  private void Update()
  {
    float playerSpeed = GetPlayerSpeed();
    if (playerSpeed <= 0.01f) return;
    _timer -= Time.deltaTime;
    if (_timer <= 0f)
    {
      Type();
    }
  }

  private void Type()
  {
    _popup.TypeCharacter(GetRandomCharacter());
    _timer = 1f / _typingSpeed;
  }

  private char GetRandomCharacter() => (char)Random.Range(32, 127);
}
