public class QuestTracker
{
  public bool completed = false;
  public int amount = 0;

  // TODO: Add something here?
  public QuestTracker(Quest _)
  {
  }
  public QuestTracker(QuestSaveData data)
  {
    completed = data.completed;
    amount = data.amount;
  }
}
