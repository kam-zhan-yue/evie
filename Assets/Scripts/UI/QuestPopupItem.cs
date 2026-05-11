using TMPro;
using UnityEngine;

public class QuestPopupItem : PopupItem<QuestUIData>
{
  [SerializeField] private TMP_Text questText;

  public override void Init(QuestUIData data) 
  {
    if (data.showCount) 
      questText.SetText($"{data.description} ({data.count} / {data.total})");
    else 
      questText.SetText($"{data.description}");
  }
}
