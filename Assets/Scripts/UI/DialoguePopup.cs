using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialoguePopup : Popup
{
  [SerializeField] private Vector2 dialogueOffset;
  [SerializeField] DialogueBox dialogueBox;

  private Camera mainCamera;

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

  public void PlayDialogue(Vector3 worldPos, DialogueUIData data)
  {
    PlayDialogueAsync(worldPos, data).Forget();
  }

  private async UniTask PlayDialogueAsync(Vector3 worldPos, DialogueUIData data)
  {
    // Convert world position to screen position
    Vector2 screenPos = mainCamera.WorldToScreenPoint(worldPos);
    Vector2 dialoguePos = screenPos + dialogueOffset;
    dialogueBox.Show(dialoguePos, data);
    ShowPopup();
    await UniTask.WaitForSeconds(2.0f);
    HidePopup();
  }
}
