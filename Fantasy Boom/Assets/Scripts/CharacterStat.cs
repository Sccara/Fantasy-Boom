using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat : MonoBehaviour
{
    public float baseValue;

    public float Value
    {
        get
        {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                value = CalculateFinalValue();
                isDirty = false;
            }

            return value;
        }
    }

    protected bool isDirty = true;
    protected float value = 0;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<Modifier> modifiers;
    public readonly ReadOnlyCollection<Modifier> Modifiers;

    public CharacterStat()
    {
        modifiers = new List<Modifier>();
        Modifiers = modifiers.AsReadOnly();
    }

    public CharacterStat(float _baseValue) : this()
    {
        baseValue = _baseValue;
    }

    public void AddModifier(Modifier mod)
    {
        isDirty = true;
        modifiers.Add(mod);
        modifiers.Sort(CompareModifierOrder);
    }

    public bool RemoveModifier(Modifier mod)
    {
        if (modifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = modifiers.Count - 1; i >= 0; i++)
        {
            if (modifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;
                modifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected float CalculateFinalValue()
    {
        Debug.Log("CalculateFinalValue");

        float finalValue = baseValue;
        float sumPercentAdd = 0;

        Debug.Log("Final value: " + finalValue);

        for (int i = 0; i < modifiers.Count; i++)
        {
            Modifier mod = modifiers[i];

            Debug.Log("Type: " + mod.modType);

            if (mod.modType == ModType.Flat)
            {
                Debug.Log("Flat mode value: " + mod.value);
                finalValue += mod.value;
                Debug.Log("Final value: " + finalValue);
            }
            else if (mod.modType == ModType.PercentAdd)
            {
                sumPercentAdd += mod.value;

                if (i + 1 >= modifiers.Count || modifiers[i + 1].modType != ModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.modType == ModType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }

    protected int CompareModifierOrder(Modifier a, Modifier b)
    {
        if (a.order < b.order)
        {
            return -1;
        }
        else if (a.order > b.order)
        {
            return 1;
        }

        return 0;
    }
}
