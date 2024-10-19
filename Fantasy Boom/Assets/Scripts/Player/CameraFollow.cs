using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
    public GameObject player;
    public Vector3 offset;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    public void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;

            transform.rotation = initialRotation;
        }
    }
}