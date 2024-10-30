using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Ability/Melee Attack")]
public class MeleeAttackAbilitySO : AbilitySO
{
    public float attackDamage;
    public float attackRange;

    private float attackRangeOffset = 1.25f;

    public override void Use(Character owner)
    {
        Collider[] hitColliders = Physics.OverlapSphere(owner.transform.position + owner.transform.forward * (attackRange * attackRangeOffset), attackRange);
        foreach (var hitCollider in hitColliders)
        {
            Character target = hitCollider.GetComponent<Character>() ?? hitCollider.GetComponentInParent<Character>();
            if (target != null && target != this)
            {
                target.OnTakeDamage(attackDamage);
            }
        }
    }
}
