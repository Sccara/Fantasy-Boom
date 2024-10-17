using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;

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
}
