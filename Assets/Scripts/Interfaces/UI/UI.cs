using Kuroneko.UtilityDelivery;
using UnityEngine;

public interface UI : IGameService
{
  public void ReportQuestComplete(QuestUIData quest);
  public void UpdateQuest(QuestUIData quest);
  public void PlayDialogue(Transform target, DialogueUIData data);
}
