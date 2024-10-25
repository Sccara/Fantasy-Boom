using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Character/AbilitySO")]
public class AbilitySO : ScriptableObject
{
    public AbilityType abilityType;
}

public enum AbilityType
{
    Attack,
    Standard,
    Ultimate
}
