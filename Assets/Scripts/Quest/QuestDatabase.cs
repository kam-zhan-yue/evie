using System.Collections.Generic;
using UnityEngine;
using ZLinq;


[CreateAssetMenu(menuName = "ScriptableObject/Quest/Quest Database", fileName = "Quest Database")]
public class QuestDatabase : ScriptableObject
{
  public List<Quest> quests = new();

  public Quest GetQuest(string id) => quests.AsValueEnumerable().First((quest) => quest.name == id);
}
