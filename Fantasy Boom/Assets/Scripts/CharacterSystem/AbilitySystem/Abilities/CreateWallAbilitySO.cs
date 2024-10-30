using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Wall Ability", menuName = "Ability/Create Wall")]
public class CreateWallAbilitySO : AbilitySO
{
    public GameObject wallPrefab;
    public float spawnRange;

    public override void Use(Character owner)
    {
        Vector3 spawnPosition = owner.transform.position + owner.transform.forward * spawnRange + new Vector3(0f, 1f, 0f);

        Instantiate(wallPrefab, spawnPosition, owner.transform.rotation);
    }
}
