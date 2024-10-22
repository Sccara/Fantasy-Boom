using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private float health;
    [SerializeField] private float mana;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float lastAttackTime = 0f;
    
    public AttackType attackType;

    private float maxHealth;

    private void Awake()
    {
        maxHealth = health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && CanAttack())
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public void OnTakeDamage(float damage)
    {
        Debug.Log("OnTakeDamage");
        health -= damage;

        healthBar.SetProgress(health / maxHealth, 3);

        if (health <= 0)
        {
            OnDied();
        }
    }

    public void Attack()
    {
        switch (attackType)
        {
            case AttackType.Melee:
                PerformMeleeAttack();
                break;
            case AttackType.Ranged:
                PerformRangedAttack();
                break;
            default:
                break;
        }
    }

    private void PerformMeleeAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"Hit Collider name {hitCollider.gameObject.name}");
            Character target = hitCollider.GetComponent<Character>() ?? hitCollider.GetComponentInParent<Character>();
            if (target != null && target != this) // Атакуем только других персонажей
            {
                target.OnTakeDamage(attackDamage);
            }
        }
    }

    private void PerformRangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, attackPosition.position, transform.rotation);

        // Добавляем параметры в снаряд
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.Initialize(transform.forward); // Передаём направление атаки

        projScript.damage = attackDamage;
        projScript.explosionRadius = explosionRadius;
        projScript.maxRange = attackRange; // Передаём дальность атаки персонажа
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    public void SetupHealthBar(Camera camera)
    {
        // Setup face camera logic
    }
}

public enum AttackType
{
    Melee,
    Ranged
}
