using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void LookAt(Transform target) => _virtualCamera.LookAt = target;

    public void Follow(Transform target) => _virtualCamera.Follow = target;
}
