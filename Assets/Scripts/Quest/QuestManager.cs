using System.Collections.Generic;
using UnityEngine;
using ZLinq;

public class QuestManager : Manager, ISaveable
{
  [SerializeField] private QuestDatabase database;

  public Dictionary<Quest, QuestTracker> _data = new();

  public override void Init()
  {
  }

  public void Load(SaveSlot saveSlot)
  {
    _data.Clear();
    foreach (QuestSaveData entry in saveSlot.questSaveData.AsValueEnumerable())
      _data.Add(database.GetQuest(entry.questId), new QuestTracker(entry));
  }

  public SaveSlot Save(SaveSlot saveSlot)
  {
    saveSlot.questSaveData = _data.AsValueEnumerable().Select(kvp => new QuestSaveData(kvp.Key, kvp.Value)).ToArray();
    return saveSlot;
  }

  public void AddQuest(Quest quest)
  {
    if (_data.ContainsKey(quest)) return;
    _data.Add(quest, new QuestTracker(quest));
  }

  public void CompleteQuest(Quest quest)
  {
    if (!_data.TryGetValue(quest, out QuestTracker tracker)) return;
    tracker.completed = true;
  }

  public void SetQuest(Quest quest, int amount)
  {
    if (!_data.TryGetValue(quest, out QuestTracker tracker)) return;
    tracker.amount = amount;
  }
}
