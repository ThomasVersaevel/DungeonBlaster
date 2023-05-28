using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat
{
  {
    public float BaseValue;

    private readonly List<StatModifier> statModifiers;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

}
public class StatModifier
{
    public readonly float Value;

    public StatModifier(float value)
    {
        Value = value;
    }
}