using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;
using ZLinq;

public class QuestManager : Manager, IQuestService, ISaveable
{
  [SerializeField] private QuestDatabase database;

  public Dictionary<Quest, QuestTracker> _data = new();

  public void Load(SaveSlot saveSlot)
  {
    _data.Clear();
    if (saveSlot.questSaveData == null) return;
    foreach (QuestSaveData entry in saveSlot.questSaveData.AsValueEnumerable())
      _data.Add(database.GetQuest(entry.questId), new QuestTracker(entry));
  }

  public SaveSlot Save(SaveSlot saveSlot)
  {
    saveSlot.questSaveData = _data.AsValueEnumerable().Select(kvp => 
      new QuestSaveData()
      {
          questId = kvp.Key.name,
          completed = kvp.Value.completed,
          amount = kvp.Value.amount
      }
    ).ToArray();
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
    if (tracker.completed) return;
    tracker.completed = true;
    ReportQuestComplete(quest);
  }

  public void IncrementQuest(Quest quest)
  {
    if (!_data.TryGetValue(quest, out QuestTracker tracker)) return;
    if (tracker.completed) return;
    tracker.amount += 1;
    if (tracker.amount > quest.amount)
      ReportQuestComplete(quest);
  }

  public void SetQuest(Quest quest, int amount)
  {
    if (!_data.TryGetValue(quest, out QuestTracker tracker)) return;
    if (tracker.completed) return;
    tracker.amount = amount;
    if (tracker.amount > quest.amount)
      ReportQuestComplete(quest);
  }

  private void ReportQuestComplete(Quest quest) 
  {
    ServiceLocator.Instance.Get<UI>().ReportQuestComplete(quest); 
  }

  public List<QuestUIData> GetUIData()
  {
    return database.quests.AsValueEnumerable().Select(quest => {
      QuestUIData data = new()
      {
          description = quest.description,
          showCount = quest.type == QuestType.Collect,
          total = quest.amount,
          completed = false,
          count = 0,
      };
      if (_data.TryGetValue(quest, out QuestTracker tracker)) 
      {
        data.completed = tracker.completed;
        data.count = tracker.amount;
      } 
      return data;
    }).ToList();
  }
}
