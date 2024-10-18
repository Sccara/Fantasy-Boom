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
        // Только владелец объекта отправляет команды серверу
        if (!IsOwner) return;

        // Получаем ввод
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Отправляем команду на сервер для обработки
        MoveServerRpc(movement, Time.deltaTime);
    }

    [ServerRpc]
    void MoveServerRpc(Vector3 movement, float deltaTime)
    {
        // Сервер сам использует deltaTime для перемещения
        transform.Translate(movement * moveSpeed * deltaTime, Space.World);
    }

    private void SetVirtualCamera()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        cam.Follow = gameObject.transform;
        cam.LookAt = gameObject.transform;
    }
}
