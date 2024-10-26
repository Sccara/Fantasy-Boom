using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public float health;
    public float mana;
    public float moveSpeed;
    public float attackDamage;
    public float attackRange;
    public float projectileRadius;
    public float explosionRadius;
    public float attackCooldown;

    public AttackType attackType;

    public GameObject projectilePrefab;
    public GameObject healthBarPrefab;
    public GameObject attackVFX;

    public AbilitySO standardAttack;
    public AbilitySO standardAbility;
    public AbilitySO ultimateAbility;
}
