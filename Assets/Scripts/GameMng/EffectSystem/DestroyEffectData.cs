//
// DestroyEffectData.cs
// 
// 2026/07/10 Created By Fate Ku
// 

using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectData
{
    public BlockType BlockType;

    public Vector2Int BlockID;

    public Vector3 Position;

    public List<GameObject> EffectObjects = new List<GameObject>();
}
