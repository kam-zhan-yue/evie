using Kuroneko.UtilityDelivery;
using TMPro;
using UnityEngine;

public class QuestPopupItem : PopupItem<QuestUIData>
{
  [SerializeField] private TMP_Text questText;
  [SerializeField] private RectTransform completedState;
  [SerializeField] private RectTransform incompletedState;

  public override void Init(QuestUIData data) 
  {
    if (data.showCount) 
      questText.SetText($"{data.description} ({data.count} / {data.total})");
    else 
      questText.SetText($"{data.description}");

    completedState.gameObject.SetActiveFast(data.completed);
    incompletedState.gameObject.SetActiveFast(!data.completed);
  }
}
