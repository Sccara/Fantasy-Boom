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
        // Только владелец объекта отправляет команды серверу
        if (!IsOwner) return;

        // Получаем ввод
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Отправляем команду на сервер для обработки
        MoveServerRpc(movement, Time.deltaTime);
    }

    [ServerRpc]
    void MoveServerRpc(Vector3 movement, float deltaTime)
    {
        // Сервер сам использует deltaTime для перемещения
        transform.Translate(movement * moveSpeed * deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            // Поворачиваем игрока в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}
