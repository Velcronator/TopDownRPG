using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _virtualCamera.Follow = PlayerController.Instance.transform;
    }
}
