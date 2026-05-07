using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct PlayerSaveData
{
  public Vector3 position;
}

[Serializable]
public struct QuestSaveData
{
  public string questId;
  public bool completed;
  public int amount;

  public QuestSaveData(Quest quest, QuestTracker tracker)
  {
    questId = quest.name;
    completed = tracker.completed;
    amount = tracker.amount;
  }
}

[Serializable]
public struct SaveSlot
{
  [HideLabel] public PlayerSaveData playerSaveData;
  [HideLabel] public QuestSaveData[] questSaveData;
}
