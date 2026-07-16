//
// RandomRes.cs
// 
// 2026/07/17 Created By Man-Yi, Yeh
//

using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomRes<T>
{
    public static T Value(Dictionary<T,float> data)
    {
        T res = default;

        float totalWeight = 0f;
        foreach (var weight in data.Values)
        {
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        foreach (var kvp in data)
        {
            cumulativeWeight += kvp.Value;
            if (randomValue <= cumulativeWeight)
            {
                res = kvp.Key;
                break;
            }
        }
        
        return res;
    }
}
