//
// NormalNearCombine.cs
// 
// 2026/06/11 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class NormalNearCombine : INearCombineStrategy
{
    public override void NearDestroy(IBlock onerBlock, IBlock destoryBlock)
    {
        Debug.Log(onerBlock.Type.ToString() + " near destroy " + destoryBlock.Type.ToString());
    }
}
