using System.Collections.Generic;
using UnityEngine;

// The purpose of the class is to listen to events and to have the player react.
// I need to set up a way for events to be triggered and events to be listened to.
// Obviously, it makes sense to set this up in an automated way as adding events
// via code can be very taxing? Probably.
// Having everything manually calling one another is a pain in the ass too.
// It would be cleaner to separate it all, but through Scriptable Objects?
// Trigger event?
//
// I mean, we would need to call ServiceLocator.Instance.Get<IEventService>().Trigger(eventID)
// EventID here can either be a string or an enum. Some would like to have everything separate, but I feel
// that having ScriptableObjects here works best.
// The flow is:
// User approaches the RiceCooker
// RiceCookerEncounterEvent
// RiceCookerActivateEvent
// as scriptableobjects
// These are played by scripts?
// Then they are listened to by an Event Listener class.
// Fuck it, enum time.
public class DialogueManager : MonoBehaviour
{
  private Dictionary<DialogueEvent, PassiveDialogue> _passiveDialogue = new();

  private void Awake()
  {
    LoadPassiveDialogue();
  }

  private void LoadPassiveDialogue()
  {
    InitPassive(new RiceCookerEncounter());
    InitPassive(new RiceCookerActivate());
  }

  private void InitPassive(PassiveDialogue dialogue)
  {
    _passiveDialogue.Add(dialogue.Event, dialogue);
  }
}
