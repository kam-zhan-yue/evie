using Kuroneko.UtilityDelivery;

public interface UI : IGameService
{
  public void ReportQuestComplete(QuestUIData quest);
  public void UpdateQuest(QuestUIData quest);
}
