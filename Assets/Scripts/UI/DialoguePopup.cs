using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialoguePopup : Popup
{
  [SerializeField] private Vector2 dialogueOffset;
  [SerializeField] DialogueBox dialogueBox;

  private Camera mainCamera;
  private Transform _target;

  protected override void Awake()
  {
    base.Awake();
    mainCamera = Camera.main;
  }

  protected override void Start()
  {
    base.Start();
    HidePopup();
  }

  private void Update() {
    if (!_showing) return;
    if (!_target) return;

    // Convert world position to screen position
    Vector2 screenPos = mainCamera.WorldToScreenPoint(_target.position);
    Vector2 dialoguePos = screenPos + dialogueOffset;
    dialogueBox.SetPosition(dialoguePos);
  }

  public void PlayDialogue(Transform target, DialogueUIData data)
  {
    PlayDialogueAsync(target, data).Forget();
  }

  private async UniTask PlayDialogueAsync(Transform target, DialogueUIData data)
  {
    _target = target;
    dialogueBox.InitData(data);
    ShowPopup();
    await UniTask.WaitForSeconds(2.0f);
    _target = null;
    HidePopup();
  }
}
