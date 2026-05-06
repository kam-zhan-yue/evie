using UnityEngine;

public class CeilingFan : MonoBehaviour
{
  [SerializeField] private Transform blades;
  [SerializeField] private float rotationsPerSecond = 1f;

  private void Awake()
  {
    if (blades == null)
    {
      Debug.LogWarning($"{name} is missing an attachment to blades.");
      enabled = false;
    }
  }

  private void Update()
  {
    blades.RotateAround(transform.position, Vector3.up, 360f * rotationsPerSecond * Time.deltaTime);
  }
}
