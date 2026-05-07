using Sirenix.OdinInspector;
using UnityEngine;

public enum QuestType
{
  Task,
  Collect,
}

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest", fileName = "New Quest")]
public class Quest : ScriptableObject
{
  public QuestType type;
  public string description;
  [HideIf("type", QuestType.Task)] public int amount;
}
