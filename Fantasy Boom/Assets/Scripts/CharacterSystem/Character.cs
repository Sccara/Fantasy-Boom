using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private CharacterSO characterConfig;
    [SerializeField] private float lastAttackTime = 0f;

    private CharacterStat health;
    private CharacterStat mana;
    private CharacterStat moveSpeed;
    private CharacterStat attackDamage;
    private CharacterStat attackRange;
    private CharacterStat attackCooldown;

    private float explosionRadius;
    private float maxHealth;

    private void Start()
    {
        InitializeCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && CanAttack())
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void InitializeCharacter()
    {
        health = new CharacterStat(characterConfig.health);
        mana = new CharacterStat(characterConfig.mana);
        moveSpeed = new CharacterStat(characterConfig.moveSpeed);
        attackDamage = new CharacterStat(characterConfig.attackDamage);
        attackRange = new CharacterStat(characterConfig.attackRange);
        attackCooldown = new CharacterStat(characterConfig.attackCooldown);

        gameObject.name = characterConfig.characterName;

        // Настраиваем панель здоровья
        //SetupHealthBar();
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown.Value;
    }

    public void OnTakeDamage(float damage)
    {
        Debug.Log("OnTakeDamage");
        health.baseValue -= damage;

        healthBar.SetProgress(health.Value / maxHealth, 3);

        if (health.Value <= 0)
        {
            OnDied();
        }
    }

    public void Attack()
    {
        switch (characterConfig.attackType)
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange.Value);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"Hit Collider name {hitCollider.gameObject.name}");
            Character target = hitCollider.GetComponent<Character>() ?? hitCollider.GetComponentInParent<Character>();
            if (target != null && target != this) // Атакуем только других персонажей
            {
                target.OnTakeDamage(attackDamage.Value);
            }
        }
    }

    private void PerformRangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, attackPosition.position, transform.rotation);

        // Добавляем параметры в снаряд
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.Initialize(transform.forward); // Передаём направление атаки

        projScript.damage = attackDamage.Value;
        projScript.explosionRadius = explosionRadius;
        projScript.maxRange = attackRange.Value; // Передаём дальность атаки персонажа
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
