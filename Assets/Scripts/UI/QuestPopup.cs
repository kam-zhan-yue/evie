using System.Collections.Generic;
using Kuroneko.UtilityDelivery;

public class QuestPopup : PopupList<QuestPopupItem, QuestUIData>
{
  protected override List<QuestUIData> GetData() => ServiceLocator.Instance.Get<IQuestService>().GetUIData();

  // TODO: Don't show all the time
  protected override void Start()
  {
    base.Start();
    ShowPopup();
    ShowPopupItems();
  }

  public void ReportQuestComplete(QuestUIData data) 
  {
    UpdateItem(data);
  }

  public void UpdateQuest(QuestUIData data) 
  {
    UpdateItem(data);
  }
}
