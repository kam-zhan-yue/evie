using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;

public class Game : MonoBehaviour
{
  [Header("Managers")]
  [SerializeField] public SaveManager saveManager;
  [SerializeField] public QuestManager questManager;
  [SerializeField] public InteractionManager interactionManager;
  [SerializeField] public UserInterface userInterface;

  private void Awake()
  {
    // Create Services
    GameObject services = new ("Services");
    DontDestroyOnLoad(services);
    Instantiate(saveManager, services.transform);
    Instantiate(questManager, services.transform);
    Instantiate(interactionManager, services.transform);

    // Register Services
    ServiceLocator serviceLocator = ServiceLocator.Instance;
    serviceLocator.Register<IQuestService>(questManager);
    serviceLocator.Register<ISaveService>(saveManager);
    serviceLocator.Register<IInteractionService>(interactionManager);
    serviceLocator.Register<UI>(userInterface);

    // Load
    saveManager.Init(new List<ISaveable>{questManager});
    saveManager.Load();
  }
}
