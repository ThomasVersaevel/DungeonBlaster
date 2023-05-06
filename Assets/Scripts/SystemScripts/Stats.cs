using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats: ScriptableObject
{
    private List<StatInfo> statInfo = new List<StatInfo>();
    public class StatInfo
    {
        public Stat statType;
        public float statValue;
    }

    public float GetStat(Stat stat)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                return s.statValue;
            }
        }
        Debug.LogError($"No stat value found for {stat} on {this.name}");
        return 0;
    }
}
