using System.Collections.Generic;

public class SaveManager : Manager, ISaveService
{
  private List<ISaveable> _saveables;

  public SaveSlot GetSaveSlot()
  {
    // TODO: Load from memory
    return new();
  }

  public void Init(List<ISaveable> saveables)
  {
    _saveables = saveables;
  }

  public void Load()
  {
    SaveSlot saveSlot = GetSaveSlot();
    foreach (ISaveable saveable in _saveables)
      saveable.Load(saveSlot);
  }

  public SaveSlot Save()
  {
    SaveSlot saveSlot = new();
    foreach (ISaveable saveable in _saveables)
      saveSlot = saveable.Save(saveSlot);
    return saveSlot;
  }
}
