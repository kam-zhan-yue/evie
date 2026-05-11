using System.Collections.Generic;
using Kuroneko.UtilityDelivery;

public interface ISaveService : IGameService
{
  public SaveSlot GetSaveSlot();
  public void Init(List<ISaveable> saveables);
  public void Load();
  public SaveSlot Save();
}
