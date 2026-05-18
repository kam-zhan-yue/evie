using Kuroneko.UtilityDelivery;

public interface IEventService : IGameService
{
  public void Register(IEventListener listener);
  public void Emit(GameEvent gameEvent);
}
