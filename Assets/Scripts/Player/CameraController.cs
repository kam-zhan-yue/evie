using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineCamera _camera;

    public CinemachineCamera Camera => _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
    }

    public void AnimateFOV(float targetFOV, float time = 0.2f)
    {
        DOTween.To(() => _camera.Lens.FieldOfView, x => _camera.Lens.FieldOfView = x, targetFOV, time);
    }
}
