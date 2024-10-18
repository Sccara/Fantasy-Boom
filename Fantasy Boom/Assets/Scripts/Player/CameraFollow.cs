using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void SetVirtualCamera(Transform player)
    {
        Debug.Log($"VCAM IS NULL: {virtualCamera == null}");
        Debug.Log($"PLAYER IS NULL: {player == null}");

        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
        Debug.Log("Setted!");
    }
}