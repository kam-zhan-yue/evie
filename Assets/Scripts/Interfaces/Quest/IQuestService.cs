using System.Collections.Generic;
using Kuroneko.UtilityDelivery;

public interface IQuestService : IGameService
{
  public List<QuestUIData> GetUIData();
}
