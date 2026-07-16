//
// RandomBool.cs
// 
// 2026/07/17 Created By Man-Yi, Yeh
//

using UnityEngine;

public class RandomBool
{
    public static bool Value(float trueProbability)
    {
        return Random.Range(0f, 1f) < trueProbability;
    }
}
