using Kuroneko.UtilityDelivery;

public interface IInteractionService : IGameService
{
  public void OnGlassBreak();
  public void OnRiceCookerOpen();
  public void OnComputerGameComplete();
  public void OnDoorOpen();
}
