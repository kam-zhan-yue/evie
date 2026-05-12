public struct QuestUIData : UIData
{
  public string id;
  public string description;
  public int count;
  public int total;
  public bool completed;
  public bool showCount;

  public string ID()
  {
    return id;
  }
}
