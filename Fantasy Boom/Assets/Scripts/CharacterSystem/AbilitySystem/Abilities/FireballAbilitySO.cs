using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball Ability", menuName = "Ability/Fireball")]
public class FireballAbilitySO : AbilitySO
{
    public GameObject fireballPrefab;
    public float speed;

    public override void Use(Character owner)
    {
        GameObject fireball = Instantiate(fireballPrefab, owner.transform.position + owner.transform.forward, owner.transform.rotation);
    }
}
