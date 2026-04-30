using UnityEngine;

public class Evie : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Player player;

    private void Awake()
    {
        player.InitCamera(cameraController);
    }
}
