using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Character/AbilitySO")]
public abstract class AbilitySO : ScriptableObject
{
    public string abilityName;
    public int manaCost;
    public float cooldown;

    public abstract void Use(Character owner);
}


