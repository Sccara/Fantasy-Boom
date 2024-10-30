using UnityEngine;

public enum ModType
{
    BaseValue,
    Flat,
    PercentAdd,
    PercentMult,
}

public enum StatType
{
    BaseValue
}

[System.Serializable]
public class Modifier : MonoBehaviour
{
    public float value;
    public ModType modType;
    public StatType statType;
    public int order;
    public readonly object source;

    public Modifier(float _value, ModType _modType, StatType _statType, int _order, object _source)
    {
        value = _value;
        modType = _modType;
        statType = _statType;
        order = _order;
        source = _source;
    }

    public Modifier(float _value, ModType _modType, StatType _statType) : this(_value, _modType, _statType, (int)_modType, null) { }

    public Modifier(float _value, ModType _modType, StatType _statType, int _order) : this(_value, _modType, _statType, _order, null) { }

    public Modifier(float _value, ModType _modType, StatType _statType, object _source) : this(_value, _modType, _statType, (int)_modType, _source) { }

    public override string ToString()
    {
        return "Value: " + value + " ModType: " + modType + " StatType: " + statType + " Order: " + order;
    }
}
