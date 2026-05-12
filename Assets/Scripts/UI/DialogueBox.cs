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

  public void Show(Vector2 position, DialogueUIData data)
  {
    _rectTransform.position = position;
    dialogueText.color = data.color;
    dialogueText.SetText(data.text);
  }
}
