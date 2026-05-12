using UnityEngine;

// TODO: Refactor this to be more flexible? The game UI is pretty simple though
public class UserInterface : MonoBehaviour, UI
{
  [SerializeField] private QuestPopup questPopup;
  [SerializeField] private DialoguePopup dialoguePopup;

  public void ReportQuestComplete(QuestUIData quest)
  {
    questPopup.ReportQuestComplete(quest);
  }

  public void UpdateQuest(QuestUIData quest)
  {
    questPopup.UpdateQuest(quest);
  }

  public void PlayDialogue(Vector3 worldPos, DialogueUIData data)
  {
    dialoguePopup.PlayDialogue(worldPos, data);
  }
}
