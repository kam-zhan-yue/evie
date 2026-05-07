using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  [Header("Managers")]
  [SerializeField] public SaveManager saveManager;
  [SerializeField] public QuestManager questManager;

  private void Awake()
  {
    GameObject managers = new ("Managers");
    DontDestroyOnLoad(managers);
    Instantiate(saveManager, managers.transform);
    Instantiate(questManager, managers.transform);
    saveManager.Load(new List<Manager>{questManager});
  }
}
