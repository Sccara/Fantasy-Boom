using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float explosionRadius;
    public float maxRange;
    public float speed = 10f; // Скорость полёта снаряда
    private Vector3 startPosition;
    private Vector3 direction; // Направление движения снаряда
    [SerializeField] private ProjectileType projectileType;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void Initialize(Vector3 direction)
    {
        this.direction = direction.normalized; // Устанавливаем направление и нормализуем вектор
    }

    private void FixedUpdate()
    {
        // Двигаем снаряд в направлении, умноженном на скорость и время кадра
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Проверяем дистанцию, которую прошёл снаряд
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject); // Уничтожаем снаряд, если он улетел за пределы радиуса атаки
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (projectileType)
        {
            case ProjectileType.Single:
                SingleHit(collision.collider);
                break;
            case ProjectileType.Explosion:
                ExplosionHit();
                break;
            default:
                break;
        }
    }

    void SingleHit(Collider hitCollider)
    {
        Debug.Log("Single Hit");
        Debug.Log($"Hit Collider name {hitCollider.gameObject.name}");
        Character target = hitCollider.GetComponent<Character>() ?? hitCollider.GetComponentInParent<Character>();
        Debug.Log($"Target is NULL: {target == null}");
        if (target != null)
        {
            target.OnTakeDamage(damage);
        }

        Destroy(gameObject);
    }

    void ExplosionHit()
    {
        Debug.Log("Explosion Hit");

        // Находим все объекты в радиусе взрыва
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"Hit Collider name {hitCollider.gameObject.name}");
            Character target = hitCollider.GetComponent<Character>() ?? hitCollider.GetComponentInParent<Character>();
            Debug.Log($"Target is NULL: {target == null}");
            if (target != null)
            {
                target.OnTakeDamage(damage);
            }
        }

        Destroy(gameObject); // Уничтожаем снаряд после взрыва
    }
}

public enum ProjectileType
{
    Single,
    Explosion
}
