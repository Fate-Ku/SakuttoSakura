//
// TimeItemNearDestroy.cs
// 
// 2026/06/11 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class TimeItemNearCombine : INearCombineStrategy
{
    public override void NearDestroy(IBlock onerBlock, IBlock destoryBlock)
    {
        onerBlock.GoDestroy();
    }
}
