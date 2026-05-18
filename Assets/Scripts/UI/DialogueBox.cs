using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
  [SerializeField] private TMP_Text dialogueText;
  private RectTransform _rectTransform;

  private void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
  }

  public void InitData(DialogueUIData data)
  {
    dialogueText.color = data.color;
    dialogueText.SetText(data.text);
  }

  public void SetPosition(Vector2 position)
  {
    _rectTransform.position = Vector2.Lerp(_rectTransform.position, position, 0.1f);
  }
}
