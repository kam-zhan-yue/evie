using Kuroneko.UtilityDelivery;

public interface IDialogueService : IGameService
{
  public void Emit(DialogueEvent dialogueEvent);
}
