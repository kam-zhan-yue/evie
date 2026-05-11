using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using ZLinq;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest Database", fileName = "Quest Database")]
public class QuestDatabase : ScriptableObject
{
  [InlineEditor]
  public List<Quest> quests = new();

  public Quest GetQuest(string id) => quests.AsValueEnumerable().First((quest) => quest.name == id);
}
