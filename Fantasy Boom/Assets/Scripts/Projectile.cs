using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float explosionRadius;
    public float maxRange;
    public float speed = 10f; // �������� ����� �������
    private Vector3 startPosition;
    private Vector3 direction; // ����������� �������� �������
    [SerializeField] private ProjectileType projectileType;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void Initialize(Vector3 direction)
    {
        this.direction = direction.normalized; // ������������� ����������� � ����������� ������
    }

    private void FixedUpdate()
    {
        // ������� ������ � �����������, ���������� �� �������� � ����� �����
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // ��������� ���������, ������� ������ ������
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject); // ���������� ������, ���� �� ������ �� ������� ������� �����
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

        // ������� ��� ������� � ������� ������
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

        Destroy(gameObject); // ���������� ������ ����� ������
    }
}

public enum ProjectileType
{
    Single,
    Explosion
}
