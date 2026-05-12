using System.Collections.Generic;
using Kuroneko.UtilityDelivery;

public interface IQuestService : IGameService
{
  public List<QuestUIData> GetUIData();
  public void IncrementQuest(Quest quest);
  public void CompleteQuest(Quest quest);
  public void SetQuest(Quest quest, int amount);
}
