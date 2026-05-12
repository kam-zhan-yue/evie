using Kuroneko.UtilityDelivery;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractionManager : Manager, IInteractionService
{
  [Header("Quests")]
  [SerializeField, InlineEditor] private Quest breakGlassQuest;

  public void OnGlassBreak()
  {
    ServiceLocator.Instance.Get<IQuestService>().IncrementQuest(breakGlassQuest);
  }
}
