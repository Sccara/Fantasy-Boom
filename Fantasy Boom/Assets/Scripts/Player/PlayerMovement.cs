using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private GameObject playerCamera;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private void Start()
    {
        if (IsLocalPlayer)
        {
            playerCamera.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        // ������ �������� ������� ���������� ������� �������
        if (!IsOwner) return;

        // �������� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // ���������� ������� �� ������ ��� ���������
        MoveServerRpc(movement, Time.deltaTime);
    }

    [ServerRpc]
    void MoveServerRpc(Vector3 movement, float deltaTime)
    {
        // ������ ��� ���������� deltaTime ��� �����������
        transform.Translate(movement * moveSpeed * deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            // ������������ ������ � ����������� ��������
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}
