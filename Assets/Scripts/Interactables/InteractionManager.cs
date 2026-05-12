using Kuroneko.UtilityDelivery;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractionManager : Manager, IInteractionService
{
  [Header("Quests")]
  [SerializeField, InlineEditor] private Quest breakGlassQuest;
  [SerializeField, InlineEditor] private Quest doorOpenQuest;
  [SerializeField, InlineEditor] private Quest videoGameQuest;
  [SerializeField, InlineEditor] private Quest riceCookerQuest;

  public void OnComputerGameComplete()
  {
    ServiceLocator.Instance.Get<IQuestService>().IncrementQuest(videoGameQuest);
  }

  public void OnDoorOpen()
  {
    ServiceLocator.Instance.Get<IQuestService>().CompleteQuest(doorOpenQuest);
  }

  public void OnGlassBreak()
  {
    ServiceLocator.Instance.Get<IQuestService>().IncrementQuest(breakGlassQuest);
  }

  public void OnRiceCookerOpen()
  {
    ServiceLocator.Instance.Get<IQuestService>().CompleteQuest(riceCookerQuest);
  }
}
