using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public float health;
    public float mana;
    public float moveSpeed;

    public List<AbilitySO> abilities;
}
