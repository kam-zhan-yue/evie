using UnityEngine;

// TODO: Refactor this to be more flexible? The game UI is pretty simple though
public class UserInterface : MonoBehaviour, UI
{
  [SerializeField] private QuestPopup questPopup;

  public void ReportQuestComplete(Quest quest)
  {
    questPopup.ReportQuestComplete(quest);
  }
}
