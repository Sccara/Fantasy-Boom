using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class PlayerMovement : NetworkBehaviour
{
    private CinemachineVirtualCamera cam;
    public float moveSpeed = 5f;

    private void Start()
    {
        SetVirtualCamera();
    }

    void FixedUpdate()
    {
        // ������ �������� ������� ���������� ������� �������
        if (!IsOwner) return;

        // �������� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // ���������� ������� �� ������ ��� ���������
        MoveServerRpc(movement, Time.deltaTime);
    }

    [ServerRpc]
    void MoveServerRpc(Vector3 movement, float deltaTime)
    {
        // ������ ��� ���������� deltaTime ��� �����������
        transform.Translate(movement * moveSpeed * deltaTime, Space.World);
    }

    private void SetVirtualCamera()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        cam.Follow = gameObject.transform;
        cam.LookAt = gameObject.transform;
    }
}
